namespace Backend_.Net.Entities
{
    public class DesignColor
    {
        public Guid Id { get; set; }
        public Guid DesignId { get; set; }
        public Design Design { get; set; } = default!;

        public Guid ColorId { get; set; }
        public Color Color { get; set; } = default!;
    }
}
