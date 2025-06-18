using Backend_.Net.DTO;
using Backend_.Net.Entities;
using Backend_.Net.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_.Net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StyleController : ControllerBase
    {
        private readonly IStyleService _styleService;

        public StyleController(IStyleService styleService)
        {
            _styleService = styleService;
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddStyle([FromBody] StyleDTO request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var style = await _styleService.AddStyleAsync(request);
            return Ok(style);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStyle()
        {
            var style = await _styleService.GetAllStyleAsync();
            return Ok(style);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStyleById(Guid id)
        {
            var style = await _styleService.GetStyleByIdAsync(id);
            if (style is null)
                return NotFound();

            return Ok(style);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStyle(Guid id, [FromBody] StyleDTO request)
        {
            var style = await _styleService.UpdateStyleAsync(id, request);
            if (style is null)
                return NotFound();

            return Ok(style);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStyle(Guid id)
        {
            var style = await _styleService.DeleteStyleAsync(id);
            if (!style)
                return NotFound();

            return Ok("removed Successfuly");
        }
    }
}
