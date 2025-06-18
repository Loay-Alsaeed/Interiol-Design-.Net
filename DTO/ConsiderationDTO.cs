using Backend_.Net.Entities;

namespace Backend_.Net.DTO
{
    public class ConsiderationDTO
    {
        public Guid DesignId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
    }
}
