namespace Backend_.Net.Entities
{
    public class Designer
    {
        public Guid Id { get; set; }
        
        public Guid UserId { get; set; }
        public User User { get; set; } = default!;
    }
}
