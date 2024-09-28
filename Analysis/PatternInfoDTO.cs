using StructuralPatternsHunter.Entities;

namespace StructuralPatternsHunter.Analysis
{
    internal struct PatternInfoDTO(string description)
    {
        public string Description { get; set; } = description;
        public ICollection<PatternItemDTO> Items { get; set; } = [];
    }

    internal struct PatternItemDTO(string name, Location location, string description)
    {
        public string Name { get; set; } = name;
        public Location Location { get; set; } = location;
        public string Description { get; set; } = description;
    }
}
