namespace Backend_.Net.Entities
{
    public class LayoutImage
    {
        public Guid Id { get; set; }
        public Guid DesignId { get; set; }
        public Design Design { get; set; } = default!;
        public required string ImageUrl {get; set;}
    }
}
