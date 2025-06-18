using System.Text.Json.Serialization;

namespace Backend_.Net.Entities
{
    public class Comment
    {
        public Guid Id { get; set; }
        public Guid UserID { get; set; }
        public User User { get; set; } = default!;

        [JsonIgnore]
        public List<DesignComment> DesignComment { get; set; } = [];
        public required string Message { get; set; }
        public DateTime DateTime { get; set; } = DateTime.UtcNow;
    }
}
