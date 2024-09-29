using System.Collections.Concurrent;
using StructuralPatternsHunter.Entities;

namespace StructuralPatternsHunter
{
    internal class EntitiesTreeCreator
    {
        public static void FillRelationships(ConcurrentDictionary<string, List<Entity>> entitiesMap)
        {
            foreach (var entity in entitiesMap.SelectMany(kvp => kvp.Value))
            {
                foreach (var parentName in entity.ParentsNames)
                {
                    var parentEntity = entitiesMap.FindEntityByName(parentName, entity.Namespace, entity.ReferencedModules);
                    if (parentEntity == null)
                    {
                        //Console.WriteLine($"Unable to find entity {parentName} referenced by {entity.Name}");
                        continue;
                    }

                    entity.Parents.Add(parentEntity);
                    parentEntity.Children.Add(entity);
                }
            }
        }
    }
}
