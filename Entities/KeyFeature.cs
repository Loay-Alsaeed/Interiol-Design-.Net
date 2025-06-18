using System.Text.Json.Serialization;

namespace Backend_.Net.Entities
{
    public class KeyFeature
    {
        public Guid Id { get; set; }
        public required string Content { get; set; }
        public Guid DesignId { get; set; }
        [JsonIgnore]
        public Design Design { get; set; } = default!;
    }
}
