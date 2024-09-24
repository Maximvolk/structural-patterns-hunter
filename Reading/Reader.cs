namespace StructuralPatternsHunter.Reading
{
    internal class Reader(string filePath)
    {
        private readonly string _filePath = filePath;

        public async IAsyncEnumerable<string> ReadLinesAsync()
        {
            using var stream = new FileStream(_filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 32768, FileOptions.Asynchronous);
            using var streamReader = new StreamReader(stream);

            string? line;
            while ((line = await streamReader.ReadLineAsync().ConfigureAwait(false)) != null)
                yield return line;
        }
    }
}
