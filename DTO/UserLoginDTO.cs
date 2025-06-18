using System.ComponentModel.DataAnnotations;

namespace Backend_.Net.DTO
{
    public class UserLoginDTO
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
