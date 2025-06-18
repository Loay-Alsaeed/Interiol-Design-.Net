using Backend_.Net.Data;
using Backend_.Net.DTO;
using Backend_.Net.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_.Net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignerController : ControllerBase
    {
        private readonly AppDbContext appDbContext;

        public DesignerController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddDesigners(DesignerDTO request)
        {
            if (!ModelState.IsValid) return BadRequest();

            var designers = new Designer
            {
                UserId = request.UserId,
            };

            await appDbContext.Designers.AddAsync(designers);
            await appDbContext.SaveChangesAsync();
            return Ok(designers);
        }
    }
}
