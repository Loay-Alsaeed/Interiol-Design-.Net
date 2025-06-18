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
    public class DesignController : ControllerBase
    {
        private readonly IDesignService _iDesignService;

        public DesignController(IDesignService iDesignService)
        {
            _iDesignService = iDesignService;
        }

        [Authorize(Roles = "Admin")]
        [Consumes("multipart/form-data")]
        [HttpPost]
        public async Task<IActionResult> CreateDesignWithDetailsAsync(AddDesignDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var design = await _iDesignService.CreateDesignWithDetailsAsync(dto);
            if (design is null) return NotFound();
            return Ok(design);
        }

        [HttpGet("{designId}")]
        public async Task<IActionResult> GetDesignByDesignId(Guid designId)
        {
            if (!ModelState.IsValid) return BadRequest();

            var design = await _iDesignService.GetDesignByDesignId(designId);
            if (design is null) return NotFound();
            return Ok(design);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDesigns()
        {
            return Ok(await _iDesignService.GetAllDesigns());
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{designId}")]
        public async Task<IActionResult> DeleteDesign(Guid designId)
        {
            await _iDesignService.DeleteDesign(designId);
            return Ok("removed successfuly");
        }
    }
}
