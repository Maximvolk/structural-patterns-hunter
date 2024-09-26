using System.Collections.Concurrent;

using StructuralPatternsHunter.Entities;
using StructuralPatternsHunter.Output;

namespace StructuralPatternsHunter.Analysis
{
    internal class Analyzer(OutputWriter outputWriter)
    {
        private readonly OutputWriter _outputWriter = outputWriter;

        private readonly List<IPatternExtractor> _extractors = new()
        {
            
        };

        public void Analyze(ConcurrentDictionary<string, List<Entity>> entitiesMap)
        {
            foreach (var (shortName, entities) in entitiesMap)
            {
                foreach (var entity in entities)
                {

                }
            }
        }

        public void ProcessAnalysis(string shortName, Entity entity)
        {
            foreach (var extractor in _extractors)
            {
                
            }
        }
    }
}
