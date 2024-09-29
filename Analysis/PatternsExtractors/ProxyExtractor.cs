using System.Collections.Concurrent;
using StructuralPatternsHunter.Entities;

namespace StructuralPatternsHunter.Analysis.PatternsExtractors
{
    internal class ProxyExtractor : IPatternExtractor
    {
        private const string PatternDescription = """
            >                        Proxy Design Pattern
            >            
            >Intent: Lets you provide a substitute or placeholder for another object. A
            >proxy controls access to the original object, allowing you to perform
            >something either before or after the request gets through to the original
            >object.
            """;

        public bool TryExtract(Entity entity, ConcurrentDictionary<string, List<Entity>> entitiesMap, out PatternInfoDTO? patternInfo)
        {
            patternInfo = null;

            if (entity.Children.Count != 2)
                return false;

            if (entity.Type != EntityType.Interface)
                return false;

            if (entity.Children.Any(c => c.Children.Count > 0))
                return false;

            Entity? proxy = null;
            Entity? realSubject = null;

            var firstChild = entity.Children.First();
            var lastChild = entity.Children.Last();

            if (firstChild.Properties.Any(p => p.Type == entity.Name || p.Type == lastChild.Name))
            {
                proxy = firstChild;
                realSubject = lastChild;
            }
            else if (lastChild.Properties.Any(p => p.Type == entity.Name || p.Type == firstChild.Name))
            {
                proxy = lastChild;
                realSubject = firstChild;
            }

            if (realSubject == null || proxy == null)
                return false;
            
            patternInfo = PrepareOutput(entity, realSubject, proxy);
            return true;
        }

        private PatternInfoDTO PrepareOutput(Entity subjectInterface, Entity realSubject, Entity proxy)
        {
            var patternInfo = new PatternInfoDTO(PatternDescription);
            patternInfo.Items.Add(new PatternItemDTO(subjectInterface.Name, subjectInterface.Locations.First(), "Subject interface"));

            patternInfo.Items.Add(new PatternItemDTO(realSubject.Name, realSubject.Locations.First(), "Real subject"));
            patternInfo.Items.Add(new PatternItemDTO(proxy.Name, proxy.Locations.First(), "Proxy"));

            return patternInfo;
        }
    }
}
