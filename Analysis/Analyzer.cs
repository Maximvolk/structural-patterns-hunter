using System.Collections.Concurrent;

using StructuralPatternsHunter.Analysis.PatternsExtractors;
using StructuralPatternsHunter.Entities;
using StructuralPatternsHunter.Output;

namespace StructuralPatternsHunter.Analysis
{
    internal class Analyzer(OutputWriter outputWriter)
    {
        private readonly List<IPatternExtractor> _extractors =
        [
            new BridgeExtractor(),
            new CompositeExtractor(),
            new DecoratorExtractor(),
            new ProxyExtractor(),
            new FacadeExtractor(),
            new FlyweightExtractor()
        ];

        public void Analyze(ConcurrentDictionary<string, List<Entity>> entitiesMap)
        {
            foreach (var (_, entities) in entitiesMap)
            {
                foreach (var entity in entities)
                    ProcessAnalysis(entity, entitiesMap);
            }
        }

        private void ProcessAnalysis(Entity entity, ConcurrentDictionary<string, List<Entity>> entitiesMap)
        {
            foreach (var extractor in _extractors)
            {
                if (extractor.TryExtract(entity, entitiesMap, out var patternInfo))
                    outputWriter.Write(patternInfo!.Value);
            }
        }
    }
}
