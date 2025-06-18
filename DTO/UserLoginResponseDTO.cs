namespace Backend_.Net.DTO
{
    public class UserLoginResponseDTO
    {
        public Guid UserId { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required string Token { get; set; }
    }
}
