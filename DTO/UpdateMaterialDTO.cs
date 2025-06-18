namespace Backend_.Net.DTO
{
    public class UpdateMaterialDTO
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string? Application { get; set; }
        public string? Supplier { get; set; }
        public string? Sustainability { get; set; }
        public string? Maintenance { get; set; }

        public IFormFile? Image { get; set; }  // اختياري
    }
}
