using System.Text;
using StructuralPatternsHunter.Reading;

namespace StructuralPatternsHunter.Parsing
{
    internal abstract class BaseParser(Tokenizer tokenizer)
    {
        protected IAsyncEnumerator<Token> _tokensEnumerator = tokenizer.TokenizeAsync().GetAsyncEnumerator();

        protected async Task<bool> TrySeekTokenAsync(params string[] tokens)
        {
            while (!tokens.Any(m => _tokensEnumerator.Current == m))
            {
                if (!await MoveNextAsync())
                    return false;
            }

            return true;
        }

        protected async Task<bool> TrySeekTokenCheckNestedAsync(string seekToken, string complementToken, StringBuilder? builder = null)
        {
            var depth = 0;

            do
            {
                builder?.Append(_tokensEnumerator.Current.Value);

                if (_tokensEnumerator.Current == complementToken)
                    depth++;
                else if (_tokensEnumerator.Current == seekToken)
                    depth--;

                if (!await MoveNextAsync())
                    return false;
            }
            while (_tokensEnumerator.Current != seekToken || depth > 1);

            builder?.Append(_tokensEnumerator.Current.Value);
            return true;
        }

        protected async Task<(bool Success, string Literal)> TryParseCompoundLiteralAsync(params string[] availableSpecialTokens)
        {
            var literalBuilder = new StringBuilder(_tokensEnumerator.Current.Value);
            var lookForDot = true;

            var angleBracketOccurred = false;
            
            while (true)
            {
                if (!await MoveNextAsync())
                    return (false, string.Empty);

                if (availableSpecialTokens.Contains(_tokensEnumerator.Current.Value))
                {
                    // Dot cannot be followed by special token
                    if (!lookForDot)
                        return (false, string.Empty);

                    if (_tokensEnumerator.Current == "<")
                    {
                        if (!await TrySeekTokenCheckNestedAsync(">", "<", literalBuilder))
                            return (false, string.Empty);

                        angleBracketOccurred = true;
                    }
                    else if (_tokensEnumerator.Current == "(")
                    {
                        if (!angleBracketOccurred)
                            return (true, literalBuilder.ToString());
                        
                        if (!await TrySeekTokenCheckNestedAsync(")", "(", literalBuilder))
                            return (false, string.Empty);
                    }

                    if (!await MoveNextAsync())
                        return (false, string.Empty);
                }

                if (lookForDot && _tokensEnumerator.Current != ".")
                    break;

                literalBuilder.Append(_tokensEnumerator.Current.Value);
                lookForDot = !lookForDot;
            }

            return (true, literalBuilder.ToString());
        }

        protected async Task<(bool Success, string Literal)> TryParseDotSeparatedLiteralAsync()
        {
            var literalBuilder = new StringBuilder(_tokensEnumerator.Current.Value);
            var lookForDot = true;

            while (true)
            {
                if (!await MoveNextAsync())
                    return (false, string.Empty);

                if (lookForDot && _tokensEnumerator.Current != ".")
                    break;

                literalBuilder.Append(_tokensEnumerator.Current.Value);
                lookForDot = !lookForDot;
            }

            return (true, literalBuilder.ToString());
        }

        protected async Task<bool> MoveNextAsync()
        {
            if (!await _tokensEnumerator.MoveNextAsync())
                return false;

            // Skip comments and preprocessor directives
            if (_tokensEnumerator.Current == "#")
                return await SkipLineAsync();

            if (_tokensEnumerator.Current != "/")
                return true;

            if (!await _tokensEnumerator.MoveNextAsync())
                return false;

            if (_tokensEnumerator.Current == "/")
                return await SkipLineAsync();

            if (_tokensEnumerator.Current == "*")
            {
                if (!await TrySeekTokenAsync("/"))
                    return false;

                return await _tokensEnumerator.MoveNextAsync();
            }

            return true;
        }

        protected async Task<bool> SkipLineAsync()
        {
            var currentLine = _tokensEnumerator.Current.Location.Line;

            while ((await _tokensEnumerator.MoveNextAsync()) && _tokensEnumerator.Current.Location.Line == currentLine) { }

            return currentLine != _tokensEnumerator.Current.Location.Line;
        }
    }
}
