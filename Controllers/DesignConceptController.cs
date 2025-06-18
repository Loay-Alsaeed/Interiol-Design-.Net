using Backend_.Net.Service;
using Backend_.Net.DTO;
using Backend_.Net.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Backend_.Net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignConceptController : ControllerBase
    {
        private readonly IDesignConceptService _designConceptService;

        public DesignConceptController(IDesignConceptService designConceptService)
        {
            _designConceptService = designConceptService;
        }

        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<DesignConcept>> AddDesignConcept([FromBody] DesignConceptDTO request)
        {
            var concept = await _designConceptService.AddDesignConceptAsync(request);
            return CreatedAtAction(nameof(GetDesignConceptById), new { id = concept?.Id }, concept);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DesignConcept>> GetDesignConceptById(Guid id)
        {
            var concept = await _designConceptService.GetDesignConceptByIdAsync(id);
            if (concept == null)
                return NotFound();
            return Ok(concept);
        }

        [HttpGet("design/{designId}")]
        public async Task<ActionResult<List<DesignConcept>>> GetConceptsByDesignId(Guid designId)
        {
            var concepts = await _designConceptService.GetDesignConceptByDesignIdAsync(designId);
            return Ok(concepts);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<DesignConcept>> UpdateDesignConcept(Guid id, [FromBody] DesignConceptDTO request)
        {
            var updated = await _designConceptService.UpdateDesignConceptAsync(id, request);
            if (updated == null)
                return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteDesignConcept(Guid id)
        {
            var success = await _designConceptService.DeleteDesignConceptAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }
    }
}
