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
    public class ConsiderationController : ControllerBase
    {
        private readonly IConsiderationService _considerationService;

        public ConsiderationController(IConsiderationService considerationService)
        {
            _considerationService = considerationService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddConsiderationAsync([FromBody]ConsiderationDTO request)
        {
            if (!ModelState.IsValid) return BadRequest();

            var cons = await _considerationService.AddConsiderationAsync(request);

            if (cons is null) return BadRequest();
            return Ok(cons);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{ConId}")]
        public async Task<IActionResult> DeleteConsiderationAsync(Guid ConId)
        {
            if (!ModelState.IsValid) return BadRequest();
            var result = await _considerationService.DeleteConsiderationAsync(ConId);

            return result ? Ok() : NotFound();
        }
        
        
        [HttpGet("design/{designId}")]
        public async Task<IActionResult> GetAllConsiderationByDesignIdAsync(Guid designId)
        {
            if (!ModelState.IsValid) return BadRequest();

            var consList = await _considerationService.GetAllConsiderationByDesignIdAsync(designId);

            if (consList is null) return NotFound();

            return Ok(consList);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllConsiderationAsync()
        {
            var allCons = await _considerationService.GetAllConsiderationAsync();

            if (allCons is null) return NotFound();
            
            return Ok(allCons);
        }

        [HttpGet("{ConId}")]
        public async Task<IActionResult> GetConsiderationByIdAsync(Guid ConId)
        {
            if (!ModelState.IsValid) return BadRequest();

            var cons = await _considerationService.GetConsiderationByIdAsync(ConId);

            if (cons is null) return NotFound();

            return Ok(cons);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{ConId}")]
        public async Task<IActionResult> UpdateConsiderationAsync(Guid ConId, [FromBody] ConsiderationDTO request)
        {
            if (!ModelState.IsValid) return BadRequest();

            var cons = await _considerationService.UpdateConsiderationAsync(ConId, request);

            if (cons is null) return NotFound();
            return Ok(cons);
        }


    }
}
