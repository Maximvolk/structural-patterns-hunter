using StructuralPatternsHunter.Analysis;

namespace StructuralPatternsHunter.Output
{
    internal class OutputWriter : IDisposable
    {
        private readonly StreamWriter _writer;

        public OutputWriter(string filePath)
        {
            if (File.Exists(filePath))
                File.Delete(filePath);

            _writer = new StreamWriter(filePath);
        }

        public void Write(PatternInfoDTO pattern)
        {
            _writer.Write("--------------------------------------------------\n");
            _writer.Write(pattern.Description);
            _writer.Write("\n\n");

            foreach (var item in pattern.Items)
            {
                var itemInfo = $"{item.Name} ({item.Location.File}, line {item.Location.Line}) -> {item.Description}\n";
                _writer.Write(itemInfo);
            }

            _writer.Flush();
        }

        public void Dispose()
        {
            _writer.Flush();
            _writer.Dispose();
        }
    }
}
