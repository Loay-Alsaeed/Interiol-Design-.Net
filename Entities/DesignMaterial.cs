using System.Text.Json.Serialization;

namespace Backend_.Net.Entities
{
    public class DesignMaterial
    {
        public Guid Id { get; set; }
        public Guid DesignId { get; set; }
        [JsonIgnore]
        public Design Design { get; set; } = default!;

        public Guid MaterialId { get; set; }
        public Material Material { get; set; } = default!;
    }
}
