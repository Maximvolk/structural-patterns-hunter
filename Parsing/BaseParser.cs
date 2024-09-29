using System.Text;
using StructuralPatternsHunter.Reading;

namespace StructuralPatternsHunter.Parsing
{
    internal abstract class BaseParser(Tokenizer tokenizer)
    {
        protected readonly IAsyncEnumerator<Token> TokensEnumerator = tokenizer.TokenizeAsync().GetAsyncEnumerator();

        protected async Task<bool> TrySeekTokenAsync(params string[] tokens)
        {
            while (!tokens.Any(m => TokensEnumerator.Current == m))
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
                builder?.Append(TokensEnumerator.Current.Value);

                if (TokensEnumerator.Current == complementToken)
                    depth++;
                else if (TokensEnumerator.Current == seekToken)
                    depth--;

                if (!await MoveNextAsync())
                    return false;
            }
            while (TokensEnumerator.Current != seekToken || depth > 1);

            builder?.Append(TokensEnumerator.Current.Value);
            return true;
        }

        protected async Task<bool> MoveNextAsync()
        {
            if (!await TokensEnumerator.MoveNextAsync())
                return false;

            if (TokensEnumerator.Current.Value == string.Empty)
                return true;

            // Skip comments and preprocessor directives
            while (TokensEnumerator.Current.Value != null &&
                (TokensEnumerator.Current.Value.StartsWith('#') || TokensEnumerator.Current.Value.StartsWith("//") || TokensEnumerator.Current.Value.StartsWith("/*")))
            {
                if (TokensEnumerator.Current.Value.StartsWith('#'))
                {
                    if (!await SkipLineAsync())
                        return false;

                    continue;
                }

                if (TokensEnumerator.Current.Value.StartsWith("//"))
                {
                    if (!await SkipLineAsync())
                        return false;

                    continue;
                }

                if (TokensEnumerator.Current == "/*")
                {
                    if (!await TrySeekTokenAsync("*/"))
                        return false;

                    if (!await TokensEnumerator.MoveNextAsync())
                        return false;
                }
            }

            return TokensEnumerator.Current.Value != null;
        }

        protected async Task<bool> SkipLineAsync()
        {
            var currentLine = TokensEnumerator.Current.Location.Line;

            while ((await TokensEnumerator.MoveNextAsync()) && TokensEnumerator.Current.Location.Line == currentLine) { }

            return currentLine != TokensEnumerator.Current.Location.Line;
        }
    }
}
