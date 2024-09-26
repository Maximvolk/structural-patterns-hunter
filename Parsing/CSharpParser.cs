using System.Text;

using StructuralPatternsHunter.Entities;
using StructuralPatternsHunter.Reading;

namespace StructuralPatternsHunter.Parsing
{
    internal class CSharpParser(Tokenizer tokenizer) : BaseParser(tokenizer), IParser
    {
        private readonly string[] _modifiers = [ "internal", "private", "public", "protected", "abstract", "virtual", "static", "readonly", "override" ];
        private readonly string[] _argumentModifiers = [ "in", "out", "ref", "this" ];

        public async IAsyncEnumerable<(string ShortName, Entity Entity)> ParseEntitiesAsync()
        {
            if (!await MoveNextAsync())
                yield break;

            var (success, referencedModules) = await TryParseReferencedModulesAsync();
            if (!success)
                yield break;

            if (!await TrySeekTokenAsync("namespace"))
                yield break;

            while (_tokensEnumerator.Current == "namespace")
            {
                if (!await MoveNextAsync())
                    yield break;

                var namespaceName = new StringBuilder(_tokensEnumerator.Current.Value);

                while ((await MoveNextAsync()) && _tokensEnumerator.Current != "{")
                    namespaceName.Append(_tokensEnumerator.Current.Value);

                if (namespaceName.Length == 0)
                    yield break;

                while (await TrySeekTokenAsync("class", "interface"))
                {
                    var entityType = _tokensEnumerator.Current == "class" ? EntityType.Class : EntityType.Interface;

                    var (shortName, entity) = await ParseEntityAsync(entityType, referencedModules, namespaceName.ToString());
                    if (entity != null)
                        yield return (shortName, entity);
                }
            }
        }

        private async Task<(bool Success, List<string> ReferencedModules)> TryParseReferencedModulesAsync()
        {
            var referencedModules = new List<string>();

            while (_tokensEnumerator.Current == "using" && (await MoveNextAsync()))
            {
                var (success, module) = await TryParseDotSeparatedLiteralAsync();
                if (success)
                    referencedModules.Add(module);
                else
                    return (false, []);

                if (_tokensEnumerator.Current == ";" && (!await MoveNextAsync()))
                    return (false, []);
            }

            return (true, referencedModules);
        }

        private async Task<(string ShortName, Entity? Entity)> ParseEntityAsync(EntityType type, List<string> referencedModules, string namespaceName)
        {
            var entityName = new StringBuilder();

            while (await MoveNextAsync() && _tokensEnumerator.Current != ":" && _tokensEnumerator.Current != "{")
            {
                if (_tokensEnumerator.Current == "(")
                {
                    if (!await TrySeekTokenCheckNestedAsync(")", "("))
                        return (string.Empty, null);

                    continue;
                }
                
                if (_tokensEnumerator.Current == "<")
                {
                    if (!await TrySeekTokenCheckNestedAsync(">", "<"))
                        return (string.Empty, null);

                    continue;
                }

                entityName.Append(_tokensEnumerator.Current.Value);
            }

            if (entityName.Length == 0)
                return (string.Empty, null);

            var entity = new Entity
            {
                Namespace = namespaceName,
                Name = entityName.ToString(),
                Type = type,
                ReferencedModules = referencedModules,
                Locations = [_tokensEnumerator.Current.Location],
            };

            if (_tokensEnumerator.Current == ":" && !await TryParseParentsAsync(entity))
                return (string.Empty, null);

            while ((await MoveNextAsync()) && _tokensEnumerator.Current != "}")
            {
                if (_tokensEnumerator.Current == ";")
                    continue;

                string? token;

                // There is a case when one more token than needed could be parsed
                // In those situations it must be processed one more time
                do
                {
                    (var success, token) = await TryParseEntityChildAsync(entity);
                    //if (!success)
                    //    return (string.Empty, null);
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

            while ((await MoveNextAsync()) && _tokensEnumerator.Current != "{")
            {
                if (_tokensEnumerator.Current == ",")
                {
                    if (nameBuilder.Length == 0)
                        return false;

                    entity.ParentsNames.Add(nameBuilder.ToString());
                    nameBuilder.Clear();
                }
                else
                    nameBuilder.Append(_tokensEnumerator.Current.Value);
            }

            if (nameBuilder.Length == 0)
                return false;

            entity.ParentsNames.Add(nameBuilder.ToString());
            return true;
        }

        private async Task<(bool Success, string? TokenToBeProcessed)> TryParseEntityChildAsync(Entity entity)
        {
            // Skip attributes ("[Attribute]")
            while (_tokensEnumerator.Current == "[")
            {
                if (!await TrySeekTokenCheckNestedAsync("]", "["))
                    return (false, null);

                if (!await MoveNextAsync())
                    return (false, null);
            }

            // Skip modifiers
            while (_modifiers.Any(m => _tokensEnumerator.Current == m))
            {
                if (!await MoveNextAsync())
                    return (false, null);
            }

            var (success, type) = await TryParseCompoundLiteralAsync("(", "<", "[");
            if (!success)
                return (false, null);

            // Constructor does not have return type. In this case we have name in type
            if (_tokensEnumerator.Current == "(")
                return (await TryParseMethodAsync(string.Empty, type, entity), null);

            var name = _tokensEnumerator.Current.Value;

            if (!await MoveNextAsync())
                return (false, null);

            // No opening parenthesis means it is a property (field)
            if (_tokensEnumerator.Current == "{" || _tokensEnumerator.Current == ";" || _tokensEnumerator.Current == "=")
                return await TryParsePropertyAsync(type, name, entity);
            else if (_tokensEnumerator.Current == "(")
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
                Location = _tokensEnumerator.Current.Location,
            });

            // Checking field conditions
            if (_tokensEnumerator.Current == ";")
                return (true, null);

            if (_tokensEnumerator.Current == "=")
                return (await TrySeekTokenAsync(";"), null);

            // At this point is must be property so checking property for correctness
            if (!await TrySeekTokenCheckNestedAsync("}", "{"))
                return (false, null);

            // After closing curly brace there still can be '='
            if (!await MoveNextAsync())
                return (false, null);

            if (_tokensEnumerator.Current == "=")
                return (await TrySeekTokenAsync(";"), null);
            // In case there was no '=' acquired token must be processed second time (so it is returned)
            
            return (true, _tokensEnumerator.Current.Value);
        }

        private async Task<bool> TryParseMethodAsync(string type, string name, Entity entity)
        {
            var method = new Method
            {
                Name = name,
                ReturnType = type,
                Location = _tokensEnumerator.Current.Location
            };

            while ((await MoveNextAsync()) && _tokensEnumerator.Current != ")")
            {
                if (_argumentModifiers.Any(m => _tokensEnumerator.Current == m) || _tokensEnumerator.Current == ",")
                    continue;

                // Skip attributes ("[Attribute]")
                while (_tokensEnumerator.Current == "[")
                {
                    if (!await TrySeekTokenCheckNestedAsync("]", "["))
                        return false;

                    if (!await MoveNextAsync())
                        return false;
                }

                var (success, argumentType) = await TryParseCompoundLiteralAsync("(", "<", "[");
                if (!success)
                    return false;

                method.Arguments.Add(new Property { Name = _tokensEnumerator.Current.Value, Type = argumentType });
            }

            if (!await MoveNextAsync())
                return false;

            if (_tokensEnumerator.Current == "{")
            {
                if (!await TrySeekTokenCheckNestedAsync("}", "{"))
                    return false;
            }
            // Handle case "void Func() => statement;"
            else if (_tokensEnumerator.Current == "=")
            {
                if (!await TrySeekTokenAsync(";"))
                    return false;
            }

            entity.Methods.Add(method);
            return true;
        }
    }
}
