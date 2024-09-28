using System.Collections.Concurrent;
using StructuralPatternsHunter.Entities;

namespace StructuralPatternsHunter.Analysis
{
    internal interface IPatternExtractor
    {
        bool TryExtract(Entity entity, ConcurrentDictionary<string, List<Entity>> entitiesMap, out PatternInfoDTO? patternInfo);
    }
}
