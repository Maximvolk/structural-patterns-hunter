using System.Collections.Concurrent;

using StructuralPatternsHunter.AST;
using StructuralPatternsHunter.Parsing;
using StructuralPatternsHunter.Reading;

namespace StructuralPatternsHunter
{
    internal class ParsingFacade(int maxParallelFiles)
    {
        private readonly SemaphoreSlim _throttlingSemaphore = new(maxParallelFiles, maxParallelFiles);

        public async Task<ConcurrentBag<ASTNode>> ParseAsync(string rootDirectory)
        {
            var nodes = new ConcurrentBag<ASTNode>();

            var parsingTasks = Directory.EnumerateFiles(rootDirectory, "*", new EnumerationOptions { RecurseSubdirectories = true })
                .Select(file => ParseFileAsync(file, nodes));

            await Task.WhenAll(parsingTasks);
            return nodes;
        }

        private async Task ParseFileAsync(string file, ConcurrentBag<ASTNode> nodes)
        {
            await _throttlingSemaphore.WaitAsync();
            
            try
            {
                var reader = new Reader(file);
                var tokenizer = new Tokenizer(reader);

                var parser = IParser.GetParser(Path.GetExtension(file), tokenizer);
                if (parser == null)
                {
                    Console.WriteLine($"Ignoring file {file}");
                    return;
                }

                await foreach (var astNode in parser.ParseNodesAsync())
                    nodes.Add(astNode);
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
