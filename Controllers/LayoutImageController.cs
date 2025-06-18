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
    public class LayoutImageController : ControllerBase
    {
        private readonly ILayoutImageService _layoutImageService;

        public LayoutImageController(ILayoutImageService layoutImageService)
        {
            _layoutImageService = layoutImageService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddlayoutImageAsync([FromForm] LayoutImageDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var layoutImage = await _layoutImageService.AddlayoutImageAsync(dto);

            if (layoutImage is false) return NotFound();

            return Ok(layoutImage);
        }

        [HttpGet("{designId}")]
        public async Task<IActionResult> GetLayoutImagesAsync(Guid designId)
        {
            if (!ModelState.IsValid) return BadRequest();

            var layoutImage = await _layoutImageService.GetLayoutImagesAsync(designId);

            if (layoutImage is null) return NotFound();

            return Ok(layoutImage);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateLayoutImageAsync([FromForm] LayoutImageDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var layoutImage = await _layoutImageService.UpdateLayoutImageAsync(dto);

            if (layoutImage is null) return NotFound();

            return Ok(layoutImage);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{designId}")]
        public async Task<IActionResult> DeleteLayoutImage(Guid designId)
        {
            if (!ModelState.IsValid) return BadRequest();

            var layoutImage = await _layoutImageService.DeleteLayoutImage(designId);

            if (layoutImage is null) return NotFound();

            return Ok(layoutImage);

        }



    }
}
