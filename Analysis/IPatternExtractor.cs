using System.Collections.Concurrent;

using StructuralPatternsHunter.Entities;
using StructuralPatternsHunter.Output;

namespace StructuralPatternsHunter.Analysis
{
    internal interface IPatternExtractor
    {
        public bool TryExtract(Entity entity, ConcurrentDictionary<string, List<Entity>> entitiesMap, out PatternInfoDTO patternInfo);
    }
}
