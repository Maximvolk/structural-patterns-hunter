namespace StructuralPatternsHunter.Entities
{
    internal record Entity
    {
        // Full name with module (e.g. Namespace.Library.ClassName)
        public string Namespace { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string FullName { get { return $"{Namespace}.{Name}"; } }

        public EntityType Type { get; set; }
        public ICollection<string> ReferencedModules { get; set; } = [];

        public ICollection<Location> Locations { get; set; } = [];

        public ICollection<string> ParentsNames { get; set; } = [];

        public ICollection<Entity> Parents { get; set; } = [];
        public ICollection<Entity> Children { get; set; } = [];

        public ICollection<Property> Properties { get; set; } = [];
        public ICollection<Method> Methods { get; set; } = [];

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
