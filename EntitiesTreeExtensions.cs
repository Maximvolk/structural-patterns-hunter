using System.Collections.Concurrent;
using StructuralPatternsHunter.Entities;

namespace StructuralPatternsHunter
{
    internal static class EntitiesTreeExtensions
    {
        public static Entity? FindEntityByName(this ConcurrentDictionary<string, List<Entity>> entitiesMap,
            string name, string entityNamespace, IEnumerable<string> referencedModules)
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
                var entity = entitiesCandidates.FirstOrDefault(e => e.Name == name && entityNamespace.StartsWith(e.Namespace));
                if (entity != null)
                    return entity;

                // Look in referenced modules
                entity = entitiesCandidates.FirstOrDefault(e => referencedModules.Any(m => e.FullName == $"{m}.{name}"));
                if (entity != null)
                    return entity;

                entity = entitiesCandidates.FirstOrDefault(e => e.FullName.EndsWith(name));
                if (entity == null)
                    Console.WriteLine($"[Warn] Unable to find entity {name}: cannot choose from several entities with same name");

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
                var entity = entitiesCandidates.FirstOrDefault(e => e.Name == nameLastWord && entityNamespace.StartsWith(e.Namespace));
                if (entity != null)
                    return entity;

                // Look in referenced modules
                entity = entitiesCandidates.FirstOrDefault(e => referencedModules.Any(m => e.FullName.EndsWith($"{m}.{name}")));
                if (entity != null)
                    return entity;

                entity = entitiesCandidates.FirstOrDefault(e => e.FullName.EndsWith(name));
                if (entity == null)
                    Console.WriteLine($"[Warn] Unable to find entity {name}: cannot choose from several entities with same name");

                return entity;
            }
        }
    }
}
