namespace Backend_.Net.DTO
{
    public class ImageDTO
    {
        public Guid DesignId { get; set; }
        public required IFormFile Image { get; set; } 
    }
}
