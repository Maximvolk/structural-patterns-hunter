namespace StructuralPatternsHunter.Entities
{
    internal struct Location(string file, int line)
    {
        public string File { get; } = file;
        public int Line { get; } = line;
    }
}
