using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Backend_.Net.Data;
using Backend_.Net.DTO;
using Backend_.Net.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Backend_.Net.Service
{
    public class AuthService : IAuthService
	{
		private readonly AppDbContext _context;
		private readonly IConfiguration _configuration;

		public AuthService(AppDbContext context, IConfiguration configuration)
		{
			_context = context;
			_configuration = configuration;
		}

        public async Task<UserLoginResponseDTO?> LoginAdmin(UserLoginDTO request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
            {
                return null; // User not found
            }
            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHashed, request.Password)
                == PasswordVerificationResult.Failed)
            {
                return null;
            }
			if (user.Role != "Admin")
			{
				return null;
			}
            var userToken = CreateToken(user);

            var userData = new UserLoginResponseDTO
            {
                UserId = user.Id,
                Email = user.Email,
                Name = user.Name,
                Phone = user.PhoneNumber,
                Token = userToken
            };
            return userData;
        }

        public async Task<UserLoginResponseDTO?> LoginAsync(UserLoginDTO request)
		{
			var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
			if (user == null)
			{
				return null; // User not found
			}

			if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHashed, request.Password)
				== PasswordVerificationResult.Failed)
			{
				return null;
			}
			var userToken = CreateToken(user);

			var userData = new UserLoginResponseDTO
			{
				UserId = user.Id,
				Email = user.Email,
				Name = user.Name,
				Phone = user.PhoneNumber,
				Token = userToken
			};

			return userData;
		}

		public async Task<User?> RegisterAsync(UserDTO request)
		{
			if (await _context.Users.AnyAsync(u => u.Email == request.Email))
			{
				return null;
			}

			User user = new User();

			var hashedPassword = new PasswordHasher<User>()
				.HashPassword(user, request.Password);

			user.Id = Guid.NewGuid();
			user.Name = request.Name;
			user.PhoneNumber = request.Phone;
			user.Email = request.Email;
			user.PasswordHashed = hashedPassword;

			_context.Users.Add(user);
			await _context.SaveChangesAsync();

			return user;
		}

		private string CreateToken(User user)
		{
			var claims = new List<Claim>
				{
					new Claim(ClaimTypes.Name, user.Name),
					new Claim(ClaimTypes.Email, user.Email),
					new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
					new Claim(ClaimTypes.Role, user.Role)
				};

			var key = new SymmetricSecurityKey(
				Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:Token")!));

			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

			var tokenDescription = new JwtSecurityToken
			(
					issuer: _configuration.GetValue<string>("AppSettings:Issuer"),
					audience: _configuration.GetValue<string>("AppSettings:Audience"),
					claims: claims,
					expires: DateTime.Now.AddDays(1),
					signingCredentials: creds
				);

			return new JwtSecurityTokenHandler().WriteToken(tokenDescription);
		}
	}
}
