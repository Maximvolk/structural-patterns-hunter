namespace StructuralPatternsHunter.Entities
{
    internal record Entity
    {
        // Full name with module (e.g. Namespace.Library.ClassName)
        public string Namespace { get; init; } = string.Empty;
        public string Name { get; init; } = string.Empty;
        public string FullName => $"{Namespace}.{Name}";

        public EntityType Type { get; init; }
        public ICollection<string> ReferencedModules { get; set; } = [];

        public ICollection<Location> Locations { get; set; } = [];

        public ICollection<string> ParentsNames { get; private set; } = [];

        public ICollection<Entity> Parents { get; } = [];
        public ICollection<Entity> Children { get; } = [];

        public ICollection<Property> Properties { get; } = [];
        public ICollection<Method> Methods { get; } = [];

        public void Merge(Entity entity)
        {
            ReferencedModules = ReferencedModules.Union(entity.ReferencedModules).ToList();
            ParentsNames = ParentsNames.Union(entity.ParentsNames).ToList();
            Locations = Locations.Union(entity.Locations).ToList();

            foreach (var property in entity.Properties)
                Properties.Add(property);

            foreach (var method in  entity.Methods)
                Methods.Add(method);
        }
    }
}
