using Backend_.Net.Entities;

namespace Backend_.Net.DTO
{
    public class CommentDTO
    {
        public Guid UserId { get; set; }
        public string Message { get; set; } = string.Empty;
        public Guid DesignId { get; set; }
    }
}
