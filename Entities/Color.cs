using System.Text.Json.Serialization;

namespace Backend_.Net.Entities
{
    public class Color
    {
        public Guid Id { get; set; }
        public Guid DesignId { get; set; }

        //[JsonIgnore]
        //public Design Design { get; set; } = default!;
        public required string Name { get; set; }
        public required string ColorNumber { get; set; }
        public string? application { get; set; }
    }
}
