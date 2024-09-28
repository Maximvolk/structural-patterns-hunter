using System.Collections.Concurrent;
using StructuralPatternsHunter.Entities;

namespace StructuralPatternsHunter.Analysis.PatternsExtractors
{
    internal class FlyweightExtractor : IPatternExtractor
    {
        private const string _patternDescription = """
            >                        Flyweight Design Pattern
            >
            >Intent: Lets you fit more objects into the available amount of RAM by sharing
            >common parts of state between multiple objects, instead of keeping all of the
            >data in each object.
            """;

        public bool TryExtract(Entity entity, ConcurrentDictionary<string, List<Entity>> entitiesMap, out PatternInfoDTO? patternInfo)
        {
            patternInfo = null;

            if (entity.Type != EntityType.Class)
                return false;

            if (entity.Children.Count > 0)
                return false;

            var commonStateCandidates = new List<Entity>();

            // Check if entity is a flyweight
            // It must contain property with common state object (we can get many candidates for them)
            foreach (var property in entity.Properties)
            {
                var commonStateCandidate = entitiesMap.FindEntityByName(property.Type, entity.Namespace, entity.ReferencedModules);

                if (commonStateCandidate != null && commonStateCandidate.Type == EntityType.Class)
                    commonStateCandidates.Add(commonStateCandidate);
            }

            foreach (var factoryCandidate in entitiesMap.SelectMany(kvp => kvp.Value))
            {
                if (!factoryCandidate.Properties.Any(p => p.Type.Contains(entity.Name) && IsArrayType(p.Type)))
                    continue;

                foreach (var commonStateCandidate in commonStateCandidates)
                {
                    if (factoryCandidate.Methods.Any(m => m.ReturnType == entity.Name && m.Arguments.Any(a => a.Type == commonStateCandidate.Name)))
                    {
                        patternInfo = PrepareOutput(entity, commonStateCandidate, factoryCandidate);
                        return true;
                    }
                }
            }

            return false;
        }

        private bool IsArrayType(string type)
        {
            return type.Contains("[]") || type.Contains("List") || type.Contains("Array") ||
                type.Contains("Enumerable") || type.Contains("Collection");
        }

        private PatternInfoDTO PrepareOutput(Entity flyweight, Entity commonState, Entity flyweightFactory)
        {
            var patternInfo = new PatternInfoDTO(_patternDescription);
            patternInfo.Items.Add(new PatternItemDTO(flyweight.Name, flyweight.Locations.First(), "Flyweight"));

            patternInfo.Items.Add(new PatternItemDTO(commonState.Name, commonState.Locations.First(), "Common State object"));
            patternInfo.Items.Add(new PatternItemDTO(flyweightFactory.Name, flyweightFactory.Locations.First(), "Flyweight Factory"));

            return patternInfo;
        }
    }
}
