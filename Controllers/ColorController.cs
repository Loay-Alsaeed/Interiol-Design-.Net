using Backend_.Net.DTO;
using Backend_.Net.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_.Net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly IColorService _colorService;
        public ColorController(IColorService colorService)
        {
            _colorService = colorService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddColor([FromBody] ColorDTO request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var color = await _colorService.AddColorAsync(request);
            return Ok(color);
        }
        
        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllColors()
        {
            var color = await _colorService.GetAllColorsAsync();
            return Ok(color);
        }

        [HttpGet("design/{designId}")]
        public async Task<IActionResult> GetColorsByDesignIdAsync(Guid designId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var colorsList = await _colorService.GetColorsByDesignIdAsync(designId);

            if (colorsList is null) return NotFound();

            return Ok(colorsList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetColorById(Guid id)
        {
            var color = await _colorService.GetColorByIdAsync(id);
            if (color is null)
                return NotFound();

            return Ok(color);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateColor(Guid id, [FromBody] ColorDTO request)
        {
            var updated = await _colorService.UpdateColorAsync(id, request);
            if (updated is null)
                return NotFound();

            return Ok(updated);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColor(Guid id)
        {
            var success = await _colorService.DeleteColorAsync(id);
            if (!success)
                return NotFound();

            return Ok("removed Successfuly");
        }
    }
}
