using System.Collections.Concurrent;

using StructuralPatternsHunter.Entities;
using StructuralPatternsHunter.Parsing;
using StructuralPatternsHunter.Reading;

namespace StructuralPatternsHunter
{
    internal class ParsingFacade(int maxParallelFiles)
    {
        private readonly SemaphoreSlim _throttlingSemaphore = new(maxParallelFiles, maxParallelFiles);

        public async Task<ConcurrentDictionary<string, List<Entity>>> ParseAsync(string rootDirectory)
        {
            var entities = new ConcurrentDictionary<string, List<Entity>>();

            var parsingTasks = Directory.EnumerateFiles(rootDirectory, "*", new EnumerationOptions { RecurseSubdirectories = true })
                .Select(file => ParseFileAsync(file, entities));

            await Task.WhenAll(parsingTasks);
            return entities;
        }

        private async Task ParseFileAsync(string file, ConcurrentDictionary<string, List<Entity>> entities)
        {
            await _throttlingSemaphore.WaitAsync();
            
            try
            {
                var reader = new Reader(file);
                var tokenizer = new Tokenizer(reader);

                var parser = IParser.GetParser(Path.GetExtension(file), tokenizer);
                if (parser == null)
                {
                    //Console.WriteLine($"Ignoring file {file}");
                    return;
                }

                await foreach (var (shortName, entity) in parser.ParseEntitiesAsync())
                {
                    // C# has partial classes so here they will be merged
                    entities.AddOrUpdate(shortName, [entity], (key, value) =>
                    {
                        var anotherEntityPart = value.FirstOrDefault(e => e.FullName == entity.FullName);

                        if (anotherEntityPart == null)
                            value.Add(entity);
                        else
                            anotherEntityPart.Merge(entity);
                        
                        return value;
                    });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"[ERROR] Error parsing file {file}: {e}");
            }
            finally
            {
                _throttlingSemaphore.Release();
            }
        }
    }
}
