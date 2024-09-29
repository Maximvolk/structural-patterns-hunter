namespace StructuralPatternsHunter.Entities
{
    internal record Method
    {
        public string Name { get; set; } = string.Empty;
        public string ReturnType { get; init; } = string.Empty;
        public ICollection<Property> Arguments { get; } = [];
        public Location Location { get; init; }
    }
}
