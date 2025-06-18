namespace Backend_.Net.Entities
{
    public class Material
    {
        public Guid Id { get; set; }
        public Guid? DesignId { get; set; } = null;
        public List<DesignMaterial> DesignMaterial { get; set; } = [];
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string ImageUrl { get; set; }
        public string? Application { get; set; }
        public string? Supplier { get; set; }
        public string? Sustainability { get; set; }
        public string? Maintenance { get; set; }
    }
}
