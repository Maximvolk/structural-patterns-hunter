using System.Collections.Concurrent;
using StructuralPatternsHunter.Entities;

namespace StructuralPatternsHunter.Analysis.PatternsExtractors
{
    internal class DecoratorExtractor : IPatternExtractor
    {
        private const string _patternDescription = """
                                    Decorator Design Pattern

            Intent: Lets you attach new behaviors to objects by placing these objects
            inside special wrapper objects that contain the behaviors.
            """;

        public bool TryExtract(Entity entity, ConcurrentDictionary<string, List<Entity>> entitiesMap, out PatternInfoDTO? patternInfo)
        {
            patternInfo = null;

            if (entity.Children.Count < 2)
                return false;

            var concreteComponents = entity.Children.Where(c => c.Children.Count == 0).ToList();
            if (concreteComponents.Count == 0)
                return false;

            if (entity.Children.Count(c => c.Children.Count > 1) != 1)
                return false;

            var baseDecorator = entity.Children.FirstOrDefault(c => c.Children.Count > 1);
            if (baseDecorator == null)
                return false;

            if (baseDecorator.Children.Any(c => c.Children.Count != 0))
                return false;

            patternInfo = PrepareOutput(entity, concreteComponents, baseDecorator);
            return true;
        }

        private PatternInfoDTO PrepareOutput(Entity baseComponent, IEnumerable<Entity> concreteComponents, Entity baseDecorator)
        {
            var patternInfo = new PatternInfoDTO(_patternDescription);
            patternInfo.Items.Add(new PatternItemDTO(baseComponent.Name, baseComponent.Locations.First(), "Base component"));

            foreach (var concreteComponent in concreteComponents)
                patternInfo.Items.Add(new PatternItemDTO(concreteComponent.Name, concreteComponent.Locations.First(), "Concrete component"));

            patternInfo.Items.Add(new PatternItemDTO(baseDecorator.Name, baseDecorator.Locations.First(), "Base decorator"));

            foreach (var concreteDecorator in baseDecorator.Children)
                patternInfo.Items.Add(new PatternItemDTO(concreteDecorator.Name, concreteDecorator.Locations.First(), "Concrete decorator"));

            return patternInfo;
        }
    }
}
