using StructuralPatternsHunter.AST;
using StructuralPatternsHunter.Reading;

namespace StructuralPatternsHunter.Parsing
{
    internal interface IParser
    {
        IAsyncEnumerable<ASTNode> ParseNodesAsync();

        static IParser? GetParser(string fileExtension, Tokenizer tokenizer)
        {
            return fileExtension switch
            {
                ".cs" => new CSharpParser(tokenizer),
                _ => null
            };
        }
    }
}
