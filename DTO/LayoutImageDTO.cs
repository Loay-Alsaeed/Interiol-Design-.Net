using Backend_.Net.Entities;

namespace Backend_.Net.DTO
{
    public class LayoutImageDTO
    {
        public Guid DesignId { get; set; }
        public required IFormFile Image { get; set; }
    }
}
