using Backend_.Net.DTO;
using Backend_.Net.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_.Net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddImageAsync([FromForm] ImageDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var image = await _imageService.AddImageAsync(dto);
            if (image is null) return NotFound();
            return Ok(image);
        }

        [HttpGet("{designId}")]
        public async Task<IActionResult> GetAllImagesAsync(Guid designId)
        {
            if (!ModelState.IsValid) return BadRequest("designId is required!!");

            var images = await _imageService.GetAllImagesAsync(designId);

            if (images is null) return NotFound();

            return Ok(images);
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("{imageId}")]
        public async Task<IActionResult> UpdateImageAsync([FromForm] ImageDTO dto, Guid imageId)
        {
            if (!ModelState.IsValid) return BadRequest();

            var images = await _imageService.UpdateImageAsync(dto, imageId);

            if (images is null) return NotFound();

            return Ok(images);
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{imageId}")]
        public async Task<IActionResult> DeleteImage(Guid imageId)
        {
            if (!ModelState.IsValid) return BadRequest();

            var images = await _imageService.DeleteImage(imageId);

            if (images is null) return NotFound();

            return Ok(images);
        }

    }
}
