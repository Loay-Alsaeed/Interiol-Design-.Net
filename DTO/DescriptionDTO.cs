using Backend_.Net.Entities;

namespace Backend_.Net.DTO
{
    public class DescriptionDTO
    {
        public Guid DesignId { get; set; }
        public required string Content { get; set; }
    }
}
