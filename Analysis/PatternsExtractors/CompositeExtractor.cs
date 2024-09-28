using System.Collections.Concurrent;
using StructuralPatternsHunter.Entities;

namespace StructuralPatternsHunter.Analysis.PatternsExtractors
{
    internal class CompositeExtractor : IPatternExtractor
    {
        private const string _patternDescription = """
            >                        Composite Design Pattern
            >
            >Intent: Lets you compose objects into tree structures and then work with
            >these structures as if they were individual objects.
            """;

        public bool TryExtract(Entity entity, ConcurrentDictionary<string, List<Entity>> entitiesMap, out PatternInfoDTO? patternInfo)
        {
            patternInfo = null;

            if (entity.Children.Count < 2)
                return false;

            var compositeComponents = new List<Entity>();
            var leafComponents = new List<Entity>();

            foreach (var child in entity.Children)
            {
                if (child.Children.Count > 0)
                    continue;

                if (child.Properties.Any(p => p.Type.Contains(entity.Name) && IsArrayType(p.Type)))
                    compositeComponents.Add(child);
                else
                    leafComponents.Add(child);
            }

            if (compositeComponents.Count > 0 && leafComponents.Count > 0)
            {
                patternInfo = PrepareOutput(entity, compositeComponents, leafComponents);
                return true;
            }

            return false;
        }

        private bool IsArrayType(string type)
        {
            return type.Contains("[]") || type.Contains("List") || type.Contains("Array") ||
                type.Contains("Enumerable") || type.Contains("Collection");
        }

        private PatternInfoDTO PrepareOutput(Entity baseComponent, IEnumerable<Entity> compositeComponents, IEnumerable<Entity> leafComponents)
        {
            var patternInfo = new PatternInfoDTO(_patternDescription);
            patternInfo.Items.Add(new PatternItemDTO(baseComponent.Name, baseComponent.Locations.First(), "Base component"));

            foreach (var compositeComponent in compositeComponents)
                patternInfo.Items.Add(new PatternItemDTO(compositeComponent.Name, compositeComponent.Locations.First(), "Concrete composite"));

            foreach (var leaftComponent in leafComponents)
                patternInfo.Items.Add(new PatternItemDTO(leaftComponent.Name, leaftComponent.Locations.First(), "Concrete leaf"));

            return patternInfo;
        }
    }
}
