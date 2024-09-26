namespace StructuralPatternsHunter.Reading
{
    internal class Reader(string filePath)
    {
        public readonly string FilePath = filePath;

        public async IAsyncEnumerable<string> ReadLinesAsync()
        {
            using var stream = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read, 32768, FileOptions.Asynchronous);
            using var streamReader = new StreamReader(stream);

            string? line;
            while ((line = await streamReader.ReadLineAsync().ConfigureAwait(false)) != null)
                yield return line;
        }
    }
}
