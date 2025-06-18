namespace Backend_.Net.DTO
{
    public class ColorDTO
    {
        public Guid DesignId { get; set; }
        public required string Name { get; set; }
        public required string ColorNumber { get; set; }
        public string? application { get; set; }
        
    }
}
