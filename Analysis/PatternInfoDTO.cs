using StructuralPatternsHunter.Entities;

namespace StructuralPatternsHunter.Analysis
{
    internal struct PatternInfoDTO(string description)
    {
        public string Description { get; } = description;
        public ICollection<PatternItemDTO> Items { get; } = [];
    }

    internal struct PatternItemDTO(string name, Location location, string description)
    {
        public string Name { get; } = name;
        public Location Location { get; } = location;
        public string Description { get; } = description;
    }
}
