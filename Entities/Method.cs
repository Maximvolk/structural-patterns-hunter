namespace StructuralPatternsHunter.Entities
{
    internal record Method
    {
        public string Name { get; set; } = string.Empty;
        public string ReturnType { get; set; } = string.Empty;
        public ICollection<Property> Arguments { get; set; } = [];
        public Location Location { get; set; }
    }
}
