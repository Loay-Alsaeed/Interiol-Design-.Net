using System.Text.Json.Serialization;

namespace Backend_.Net.Entities
{
    public class Consideration
    {
        public Guid Id { get; set; }
        public Guid DesignId { get; set; }
        [JsonIgnore]
        public Design Design { get; set; } = default!;
        public required string Title { get; set; }
        public required string Description { get; set; }
    }
}
