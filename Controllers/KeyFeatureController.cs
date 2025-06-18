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
    public class KeyFeatureController : ControllerBase
    {
        private readonly IKeyFeatureService _keyFeatureService;

        public KeyFeatureController(IKeyFeatureService keyFeatureService)
        {
            _keyFeatureService = keyFeatureService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddKeyfeatureAsync([FromBody] KeyFeatureDTO request)
        {
            if (!ModelState.IsValid) return BadRequest();

            var key = await _keyFeatureService.AddKeyFeatureAsync(request);

            if (key is null) return BadRequest();
            return Ok(key);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllKeyFeatureAsync()
        {
            var allKey = await _keyFeatureService.GetAllKeyFeatureAsync();

            if (allKey is null) return NotFound();

            return Ok(allKey);
        }

        [HttpGet("design/{designId}")]
        public async Task<IActionResult> GetAllKeyFeatureByDesignIdAsync(Guid designId)
        {
            if (!ModelState.IsValid) return BadRequest();

            var keyList = await _keyFeatureService.GetAllKeyFeatureByDesignIdAsync(designId);

            if (keyList is null) return NotFound();

            return Ok(keyList);
        }

        [HttpGet("{keyId}")]
        public async Task<IActionResult> GetKeyFeatureByIdAsync(Guid keyId)
        {
            if (!ModelState.IsValid) return BadRequest();

            var key = await _keyFeatureService.GetKeyFeatureByIdAsync(keyId);

            if (key is null) return NotFound();

            return Ok(key);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{keyId}")]
        public async Task<IActionResult> UpdateKeyFeatureAsync(Guid keyId, KeyFeatureDTO request)
        {
            if (!ModelState.IsValid) return BadRequest();

            var newKey = await _keyFeatureService.UpdateKeyFeatureAsync(keyId, request);

            if (newKey is null) return NotFound();
            
            return Ok(newKey);
                   
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{keyId}")]
        public async Task<IActionResult> DeleteKeyFeatureAsync(Guid keyId)
        {
            if (!ModelState.IsValid) return BadRequest();
            var result = await _keyFeatureService.DeleteKeyFeatureAsync(keyId);

            return result ? Ok() : NotFound();
        }

    }
}
