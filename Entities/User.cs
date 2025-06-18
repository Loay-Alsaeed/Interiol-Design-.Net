using System.ComponentModel.DataAnnotations;

namespace Backend_.Net.Entities
{
    public class User
    {
        public Guid Id { get; set; } = new Guid();

        public string? Name { get; set; }

        public string? Email { get; set; }
        public string? PhoneNumber { get; set; } 

        public string? PasswordHashed { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Role { get; set; } = "User";
        public string? ResetToken { get; set; }
        public DateTime? ResetTokenExpiresAt { get; set; }
    }
}
