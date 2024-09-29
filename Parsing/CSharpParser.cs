using System.Text;

using StructuralPatternsHunter.Entities;
using StructuralPatternsHunter.Reading;

namespace StructuralPatternsHunter.Parsing
{
    internal class CSharpParser(Tokenizer tokenizer) : BaseParser(tokenizer), IParser
    {
        private readonly string[] _modifiers = [ "internal", "private", "public", "protected", "abstract", "virtual", "static", "readonly", "override" ];
        private readonly string[] _argumentModifiers = [ "in", "out", "ref", "this", "params" ];

        public async IAsyncEnumerable<(string ShortName, Entity Entity)> ParseEntitiesAsync()
        {
            if (!await MoveNextAsync())
                yield break;

            var (success, referencedModules) = await TryParseReferencedModulesAsync();
            if (!success)
                yield break;

            if (!await TrySeekTokenAsync("namespace"))
                yield break;

            while (TokensEnumerator.Current == "namespace")
            {
                if (!await MoveNextAsync())
                    yield break;

                var namespaceName = TokensEnumerator.Current.Value;
                if (namespaceName.Length == 0)
                    yield break;

                while (await TrySeekTokenAsync("class", "interface"))
                {
                    var entityType = TokensEnumerator.Current == "class" ? EntityType.Class : EntityType.Interface;

                    var (shortName, entity) = await ParseEntityAsync(entityType, referencedModules, namespaceName);
                    if (entity != null)
                        yield return (shortName, entity);
                }
            }
        }

        private async Task<(bool Success, List<string> ReferencedModules)> TryParseReferencedModulesAsync()
        {
            var referencedModules = new List<string>();

            while (TokensEnumerator.Current == "using" && (await MoveNextAsync()))
            {
                referencedModules.Add(TokensEnumerator.Current.Value);

                if (!await MoveNextAsync() || TokensEnumerator.Current != ";")
                    return (false, []);

                if (!await MoveNextAsync())
                    return (false, []);
            }

            return (true, referencedModules);
        }

        private async Task<(string ShortName, Entity? Entity)> ParseEntityAsync(EntityType type, List<string> referencedModules, string namespaceName)
        {
            var entityName = new StringBuilder();
            var entityLocation = TokensEnumerator.Current.Location;

            while (await MoveNextAsync() && TokensEnumerator.Current != ":" && TokensEnumerator.Current != "{")
            {
                if (TokensEnumerator.Current == "(")
                {
                    if (!await TrySeekTokenCheckNestedAsync(")", "("))
                        return (string.Empty, null);

                    continue;
                }

                entityName.Append(TokensEnumerator.Current.Value);
            }

            if (entityName.Length == 0)
                return (string.Empty, null);

            var entity = new Entity
            {
                Namespace = namespaceName,
                Name = entityName.ToString(),
                Type = type,
                ReferencedModules = referencedModules,
                Locations = [entityLocation],
            };

            if (TokensEnumerator.Current == ":" && !await TryParseParentsAsync(entity))
                return (string.Empty, null);

            while ((await MoveNextAsync()) && TokensEnumerator.Current != "}")
            {
                if (TokensEnumerator.Current == ";")
                    continue;

                string? token;

                // There is a case when one more token than needed could be parsed
                // In those situations it must be processed one more time
                do
                {
                    (var _, token) = await TryParseEntityChildAsync(entity);
                }
                while (token != null && token != "}");

                if (token == "}")
                    return (entityName.ToString(), entity);
            }

            return (entityName.ToString(), entity);
        }

        private async Task<bool> TryParseParentsAsync(Entity entity)
        {
            var nameBuilder = new StringBuilder();

            while ((await MoveNextAsync()) && TokensEnumerator.Current != "{")
            {
                if (TokensEnumerator.Current == ",")
                {
                    if (nameBuilder.Length == 0)
                        return false;

                    entity.ParentsNames.Add(nameBuilder.ToString());
                    nameBuilder.Clear();
                }
                else
                    nameBuilder.Append(TokensEnumerator.Current.Value);
            }

            if (nameBuilder.Length == 0)
                return false;

            entity.ParentsNames.Add(nameBuilder.ToString());
            return true;
        }

        private async Task<(bool Success, string? TokenToBeProcessed)> TryParseEntityChildAsync(Entity entity)
        {
            // Skip attributes ("[Attribute]")
            while (TokensEnumerator.Current.Value.StartsWith('[') && TokensEnumerator.Current.Value.EndsWith(']'))
            {
                if (!await MoveNextAsync())
                    return (false, null);
            }

            // Skip modifiers
            while (_modifiers.Any(m => TokensEnumerator.Current == m))
            {
                if (!await MoveNextAsync())
                    return (false, null);
            }

            var type = TokensEnumerator.Current.Value;
            if (!await MoveNextAsync())
                return (false, null);

            // Constructor does not have return type. In this case we have name in type
            if (TokensEnumerator.Current == "(")
                return (await TryParseMethodAsync(string.Empty, type, entity), null);

            var name = TokensEnumerator.Current.Value;

            if (!await MoveNextAsync())
                return (false, null);

            // No opening parenthesis means it is a property (field)
            if (TokensEnumerator.Current == "{" || TokensEnumerator.Current == ";" || TokensEnumerator.Current == "=")
                return await TryParsePropertyAsync(type, name, entity);
            else if (TokensEnumerator.Current == "(")
                return (await TryParseMethodAsync(type, name, entity), null);
            else
                return (false, null);
        }

        private async Task<(bool Success, string? TokenToBeProcessed)> TryParsePropertyAsync(string type, string name, Entity entity)
        {
            entity.Properties.Add(new Property
            {
                Name = name,
                Type = type,
                Location = TokensEnumerator.Current.Location,
            });

            // Checking field conditions
            if (TokensEnumerator.Current == ";")
                return (true, null);

            if (TokensEnumerator.Current == "=")
                return (await TrySeekTokenAsync(";"), null);

            // At this point is must be property so checking property for correctness
            if (!await TrySeekTokenCheckNestedAsync("}", "{"))
                return (false, null);

            // After closing curly brace there still can be '='
            if (!await MoveNextAsync())
                return (false, null);

            if (TokensEnumerator.Current == "=")
                return (await TrySeekTokenAsync(";"), null);
            // In case there was no '=' acquired token must be processed second time (so it is returned)
            
            return (true, TokensEnumerator.Current.Value);
        }

        private async Task<bool> TryParseMethodAsync(string type, string name, Entity entity)
        {
            var method = new Method
            {
                Name = name,
                ReturnType = type,
                Location = TokensEnumerator.Current.Location
            };

            while ((await MoveNextAsync()) && TokensEnumerator.Current != ")")
            {
                if (_argumentModifiers.Any(m => TokensEnumerator.Current == m) || TokensEnumerator.Current == ",")
                    continue;
                
                // Skip attributes ("[Attribute]")
                while (TokensEnumerator.Current.Value.StartsWith('[') && TokensEnumerator.Current.Value.EndsWith(']'))
                {
                    if (!await MoveNextAsync())
                        return false;
                }

                var argumentType = TokensEnumerator.Current.Value;
                if (!await MoveNextAsync())
                    return false;

                method.Arguments.Add(new Property { Name = TokensEnumerator.Current.Value, Type = argumentType });
            }

            if (!await MoveNextAsync())
                return false;

            if (TokensEnumerator.Current == ":")
            {
                if (!await TrySeekTokenAsync("{"))
                    return false;
            }

            if (TokensEnumerator.Current == "{")
            {
                if (!await TrySeekTokenCheckNestedAsync("}", "{"))
                    return false;
            }
            // Handle case "void Func() => statement;"
            else if (TokensEnumerator.Current == "=>")
            {
                if (!await TrySeekTokenAsync(";"))
                    return false;
            }

            entity.Methods.Add(method);
            return true;
        }
    }
}
