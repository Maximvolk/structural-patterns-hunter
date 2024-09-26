namespace StructuralPatternsHunter.Entities
{
    internal struct Location(string file, int line)
    {
        public string File { get; set; } = file;
        public int Line { get; set; } = line;
    }
}
