using System.Text;

namespace StructuralPatternsHunter.Reading
{
    internal class Tokenizer(Reader reader)
    {
        private readonly Reader _reader = reader;
        private readonly char[] _symbolsToIgnore = [ ' ', '\n', '\r', '\t' ];

        public async IAsyncEnumerable<string> TokenizeAsync()
        {
            await foreach (var line in _reader.ReadLinesAsync())
            {
                var currentToken = new StringBuilder();

                char? previousSymbol = null;
                var quotesOpened = false;

                foreach (var symbol in line)
                {
                    if (symbol == '"' || symbol == '\'')
                    {
                        if (previousSymbol == '\\')
                            currentToken.Append(symbol);
                        else
                            quotesOpened = !quotesOpened;
                    }
                    else if (quotesOpened || char.IsLetterOrDigit(symbol) || symbol == '_')
                    {
                        currentToken.Append(symbol);
                    }
                    else
                    {
                        if (currentToken.Length > 0)
                            yield return currentToken.ToString().Trim();

                        if (!_symbolsToIgnore.Contains(symbol))
                            yield return symbol.ToString();

                        currentToken.Clear();
                    }

                    previousSymbol = symbol;
                }

                if (currentToken.Length > 0)
                    yield return currentToken.ToString().Trim();
            }
        }
    }
}
