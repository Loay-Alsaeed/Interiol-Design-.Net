using Backend_.Net.DTO;
using Backend_.Net.Entities;
using Backend_.Net.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend_.Net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDTO request)
        {
            var user = await authService.RegisterAsync(request);
            if (user == null)
            {
                return BadRequest("user already exists");
            }
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserLoginResponseDTO>> Login(UserLoginDTO request)
        {
            var user = await authService.LoginAsync(request);
            if (user == null)
            {
                return BadRequest("Invalid Email or password.");
            }
            return Ok(user);
        }
        
        [HttpPost("Admin")]
        public async Task<ActionResult<UserLoginResponseDTO>> LoginAdmin(UserLoginDTO request)
        {
            var user = await authService.LoginAdmin(request);
            if (user is null) return NotFound("Invalid Email or Password");
            return Ok(user);
        }

        [Authorize]
        [HttpGet]
        public IActionResult AuthenticatedOnlyEndPoint()
        {
            return Ok("you are Authenticated");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Admin")]
        public IActionResult AdminOnlyEndPoint()
        {
            return Ok("you are Admin");
        }
    }
}
