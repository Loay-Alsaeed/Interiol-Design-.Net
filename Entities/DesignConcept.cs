using System.Text.Json.Serialization;

namespace Backend_.Net.Entities
{
    public class DesignConcept
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid DesignId { get; set; }

        [JsonIgnore]
        public Design Design { get; set; } = default!;
        public required string Concept { get; set; }

    }
}
