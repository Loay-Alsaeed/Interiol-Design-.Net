namespace Backend_.Net.DTO
{
    public class AddColorDTO
    {
        public required string Name { get; set; }
        public required string ColorNumber { get; set; }
        public string? application { get; set; }
    }
}
