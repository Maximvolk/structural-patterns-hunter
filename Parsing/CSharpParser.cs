using StructuralPatternsHunter.AST;
using StructuralPatternsHunter.Reading;

namespace StructuralPatternsHunter.Parsing
{
    internal class CSharpParser(Tokenizer tokenizer) : IParser
    {
        private readonly Tokenizer _tokenizer = tokenizer;

        public async IAsyncEnumerable<ASTNode> ParseNodesAsync()
        {
            var tokens = new List<string>();
            await foreach (var token in _tokenizer.TokenizeAsync())
                tokens.Add(token);

            yield return new ASTNode();
        }
    }
}
