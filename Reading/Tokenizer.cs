using System.Text;

namespace StructuralPatternsHunter.Reading
{
    internal class Tokenizer(Reader reader)
    {
        private readonly Reader _reader = reader;
        private readonly char[] _symbolsToIgnore = [ ' ', '\n', '\r', '\t' ];
        private readonly char[] _brakeSymbols = [ ' ', '\n', '\r', '\t', ';', '(', ')', '{', '}', ',', ':' ];

        public async IAsyncEnumerable<Token> TokenizeAsync()
        {
            var lineNumber = 1;

            await foreach (var line in _reader.ReadLinesAsync())
            {
                var currentToken = new StringBuilder();

                char? previousSymbol = null;
                var quotesOpened = false;
                var angleBracketsDepth = 0;

                foreach (var symbol in line)
                {
                    if (symbol == '"' || symbol == '\'')
                    {
                        if (previousSymbol == '\\')
                            currentToken.Append(symbol);
                        else
                            quotesOpened = !quotesOpened;
                    }
                    else if (symbol == '<')
                    {
                        currentToken.Append(symbol);
                        angleBracketsDepth++;
                    }
                    else if (symbol == '>')
                    {
                        currentToken.Append(symbol);
                        angleBracketsDepth--;
                    }
                    else if (quotesOpened || angleBracketsDepth > 0 || !_brakeSymbols.Contains(symbol))
                    {
                        currentToken.Append(symbol);

                        var currentTokenString = currentToken.ToString();
                        if (currentTokenString == "//" || currentTokenString == "/*" || currentTokenString == "*/")
                        {
                            yield return new Token(currentTokenString, lineNumber, _reader.FilePath);
                            currentToken.Clear();
                        }
                    }
                    else
                    {
                        if (currentToken.Length > 0)
                            yield return new Token(currentToken.ToString().Trim(), lineNumber, _reader.FilePath);

                        if (!_symbolsToIgnore.Contains(symbol))
                            yield return new Token(symbol.ToString(), lineNumber, _reader.FilePath);

                        currentToken.Clear();
                    }

                    previousSymbol = symbol;
                }

                if (currentToken.Length > 0)
                    yield return new Token(currentToken.ToString().Trim(), lineNumber, _reader.FilePath);

                lineNumber++;
            }
        }
    }
}
