using System.Collections.Concurrent;
using StructuralPatternsHunter.Entities;

namespace StructuralPatternsHunter.Analysis.PatternsExtractors
{
    internal class BridgeExtractor : IPatternExtractor
    {
        private const string PatternDescription = """
            >                        Bridge Design Pattern
            >
            >Intent: Lets you split a large class or a set of closely related classes into
            >two separate hierarchies—abstraction and implementation—which can be
            >developed independently of each other.
            >
            >              A
            >           /     \                        A         N
            >         Aa      Ab        ===>        /     \     / \
            >        / \     /  \                 Aa(N) Ab(N)  1   2
            >      Aa1 Aa2  Ab1 Ab2
            """;

        public bool TryExtract(Entity entity, ConcurrentDictionary<string, List<Entity>> entitiesMap, out PatternInfoDTO? patternInfo)
        {
            patternInfo = null;

            if (entity.Properties.Count == 0 || entity.Children.Count == 0)
                return false;

            // Considering entity as base Abstraction class
            // Check base class has property which is of base implementation type
            foreach (var property in entity.Properties)
            {
                var implementationCandidate = entitiesMap.FindEntityByName(property.Type, entity.Namespace, entity.ReferencedModules);
                if (implementationCandidate == null)
                    continue;

                if (!IsImplementationHierarchy(implementationCandidate))
                    continue;
                
                patternInfo = PrepareOutput(entity, implementationCandidate);
                return true;
            }

            // In case base class does not have property of base implementation type, children properties must be checked
            // Base implementation type property must be common for all base abstraction class children
            var childrenProperties = entity.Children.SelectMany(c => c.Properties).Select(p => p.Type);
            var commonProperties = childrenProperties.Where(p => entity.Children.All(c => c.Properties.Any(cp => cp.Type == p)));

            foreach (var property in commonProperties)
            {
                var implementationCandidate = entitiesMap.FindEntityByName(property, entity.Namespace, entity.ReferencedModules);
                if (implementationCandidate == null)
                    continue;

                if (!IsImplementationHierarchy(implementationCandidate))
                    continue;
                
                patternInfo = PrepareOutput(entity, implementationCandidate);
                return true;
            }

            return false;
        }

        private bool IsImplementationHierarchy(Entity entity)
        {
            if (entity.Type != EntityType.Interface)
                return false;

            return entity.Children.Count > 1;
        }

        private PatternInfoDTO PrepareOutput(Entity baseAbstraction, Entity implementationInterface)
        {
            var patternInfo = new PatternInfoDTO(PatternDescription);

            // Add base abstraction and its extensions
            patternInfo.Items.Add(new PatternItemDTO(baseAbstraction.Name, baseAbstraction.Locations.First(), "A"));

            foreach (var extendedAbstraction in baseAbstraction.Children)
                patternInfo.Items.Add(new PatternItemDTO(extendedAbstraction.Name, extendedAbstraction.Locations.First(), "Ai(N)"));

            // Add base implementation with concrete ones
            patternInfo.Items.Add(new PatternItemDTO(implementationInterface.Name, implementationInterface.Locations.First(), "N"));

            foreach (var concreteImpl in implementationInterface.Children)
                patternInfo.Items.Add(new PatternItemDTO(concreteImpl.Name, concreteImpl.Locations.First(), "Ni"));

            return patternInfo;
        }
    }
}
