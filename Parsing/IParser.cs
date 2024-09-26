using StructuralPatternsHunter.Entities;
using StructuralPatternsHunter.Reading;

namespace StructuralPatternsHunter.Parsing
{
    internal interface IParser
    {
        IAsyncEnumerable<(string ShortName, Entity Entity)> ParseEntitiesAsync();

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
