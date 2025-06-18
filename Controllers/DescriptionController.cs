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
    public class DescriptionController : ControllerBase
    {
        private readonly IDescriptionService _designService;

        public DescriptionController(IDescriptionService designService)
        {
            _designService = designService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddDescriptionAsync(DescriptionDTO request)
        {
            if (!ModelState.IsValid) return BadRequest();
            var result = await _designService.AddDescriptionAsync(request);
            return result == null ? NotFound() : Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDescriptionAsync(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest();
            var result = await _designService.DeleteDescriptionAsync(id);
            return result ? Ok() : NotFound();
        }

        [HttpGet("design/{designId}")]
        public async Task<IActionResult> GetDescriptionBtDesignIdAsync(Guid designId)
        {
            if (!ModelState.IsValid) return BadRequest();

            var description = await _designService.GetDescriptionBtDesignIdAsync(designId);

            return description == null ? NotFound() : Ok(description);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDescriptionByIdAsync(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest();

            var description = await _designService.GetDescriptionByIdAsync(id);

            return description == null ? NotFound() : Ok(description);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateDescriptionAsync(Guid id, DescriptionDTO request)
        {
            if (!ModelState.IsValid) return BadRequest();

            var description = await _designService.UpdateDescriptionAsync(id, request);

            return description == null ? NotFound() : Ok(description);
        }
    }
}
