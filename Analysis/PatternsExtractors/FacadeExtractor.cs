using System.Collections.Concurrent;
using StructuralPatternsHunter.Entities;

namespace StructuralPatternsHunter.Analysis.PatternsExtractors
{
    internal class FacadeExtractor : IPatternExtractor
    {
        private const string _patternDescription = """
            >                        Facade Design Pattern
            >
            >Intent: Provides a simplified interface to a library, a framework, or any
            >other complex set of classes.
            """;

        public bool TryExtract(Entity entity, ConcurrentDictionary<string, List<Entity>> entitiesMap, out PatternInfoDTO? patternInfo)
        {
            patternInfo = null;

            if (entity.Type != EntityType.Class)
                return false;

            if (entity.Children.Count > 0)
                return false;

            if (entity.Methods.Count == 0)
                return false;

            var subsystems = new List<Entity>();

            foreach (var property in entity.Properties)
            {
                var subsystemCandidate = entitiesMap.FindEntityByName(property.Type, entity.Namespace, entity.ReferencedModules);
                if (subsystemCandidate == null)
                    continue;

                if (subsystemCandidate.Type == EntityType.Interface || (subsystemCandidate.Children.Count == 0 && subsystemCandidate.Methods.Count > 0))
                    subsystems.Add(subsystemCandidate);
            }

            subsystems = subsystems.DistinctBy(s => s.Name).ToList();

            if (subsystems.Count > 1)
            {
                patternInfo = PrepareOutput(entity, subsystems);
                return true;
            }

            return false;
        }

        private PatternInfoDTO PrepareOutput(Entity facade, IEnumerable<Entity> subsystems)
        {
            var patternInfo = new PatternInfoDTO(_patternDescription);
            patternInfo.Items.Add(new PatternItemDTO(facade.Name, facade.Locations.First(), "Facade"));

            foreach (var subsystem in subsystems)
                patternInfo.Items.Add(new PatternItemDTO(subsystem.Name, subsystem.Locations.First(), "Subsystem"));

            return patternInfo;
        }
    }
}
