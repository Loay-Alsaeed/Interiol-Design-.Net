using System.ComponentModel.DataAnnotations;

namespace Backend_.Net.DTO
{
    public class UserDTO
    {
        public required string Name { get; set; }
        public required string Phone { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
