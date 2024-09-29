namespace StructuralPatternsHunter.Entities
{
    internal record Property
    {
        public string Name { get; set; } = string.Empty;
        public string Type { get; init; } = string.Empty;

        public Location Location { get; init; }
    }
}
