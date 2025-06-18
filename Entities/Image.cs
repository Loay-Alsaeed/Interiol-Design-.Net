using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Backend_.Net.Entities
{
    public class Image
    {
        public Guid Id { get; set; }
        public required string ImageUrl { get; set; }
        public Guid DesignId { get; set; }

        [JsonIgnore]
        public Design Design { get; set; } = default!;
    }
}
