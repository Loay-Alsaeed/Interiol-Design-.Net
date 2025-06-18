namespace Backend_.Net.Entities
{
    public class DesignComment
    {
        public Guid Id { get; set; }
        public Guid DesignId { get; set; }
        public Design Design { get; set; } = default!;

        public Guid CommentId { get; set; }
        public Comment Comment { get; set; } = default!;
    }
}
