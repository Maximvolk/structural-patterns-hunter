using System.Collections.Concurrent;
using StructuralPatternsHunter.Entities;

namespace StructuralPatternsHunter
{
    internal class EntitiesTreeCreator
    {
        public void FillRelationships(ConcurrentDictionary<string, List<Entity>> entitiesMap)
        {
            foreach (var entity in entitiesMap.SelectMany(kvp => kvp.Value))
            {
                foreach (var parentName in entity.ParentsNames)
                {
                    var parentEntity = FindEntityByName(parentName, entity.Namespace, entity.ReferencedModules, entitiesMap);
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

        private Entity? FindEntityByName(string name, string entityNamespace, IEnumerable<string> referencedModules,
            ConcurrentDictionary<string, List<Entity>> entitiesMap)
        {
            var nameSpan = name.AsSpan();

            // Name contains one word (so it is the short one and must be a key from entities map)
            if (nameSpan.Count('.') == 0)
            {
                // If short name is not found in the map then it is library or system so must be ignored
                if (!entitiesMap.TryGetValue(name, out var entitiesCandidates))
                    return null;

                if (entitiesCandidates.Count == 1)
                    return entitiesCandidates.First();

                // Look in local namespace
                var entity = entitiesCandidates.FirstOrDefault(e => e.Name == name && e.Namespace == entityNamespace);
                if (entity != null)
                    return entity;

                // Look in referenced modules
                entity = entitiesCandidates.FirstOrDefault(e => referencedModules.Any(m => e.FullName == $"{m}.{name}"));
                if (entity == null)
                    Console.WriteLine($"[Warn] Unable to find entity by (short name) {name}: cannot choose from several entities with same name");

                return entity;
            }
            else
            {
                // Take the last word from name and look in the map
                var nameLastWord = nameSpan[(name.LastIndexOf('.') + 1)..].ToString();
                if (!entitiesMap.TryGetValue(nameLastWord, out var entitiesCandidates))
                    return null;

                if (entitiesCandidates.Count == 1)
                    return entitiesCandidates.First();

                // Look in local namespace
                var entity = entitiesCandidates.FirstOrDefault(e => e.Name == nameLastWord && e.Namespace == entityNamespace);
                if (entity != null)
                    return entity;

                // Look in referenced modules
                entity = entitiesCandidates.FirstOrDefault(e => referencedModules.Any(m => e.FullName == $"{m}.{name}"));
                if (entity == null)
                    Console.WriteLine($"[Warn] Unable to find entity by (short name) {name}: cannot choose from several entities with same name");

                return entity;
            }
        }
    }
}
