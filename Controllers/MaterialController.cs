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
    public class MaterialController : ControllerBase
    {
        private readonly IMaterialService _materialService;

        public MaterialController(IMaterialService materialService)
        {
            _materialService = materialService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] MaterialDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest("invalid input data.");

            var material = await _materialService.Create(dto);
            
            if (material is null) return BadRequest("Image is required.");

            return Ok(material);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var material = await _materialService.GetAll();
            if (material is null) return NotFound();
            return Ok(material);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var material = await _materialService.Get(id);
            if (material is null) return NotFound();
            return Ok(material);
        }

        [HttpGet("design/{designId}")]
        public async Task<IActionResult> GetAllByDesignId(Guid designId)
        {
            if (!ModelState.IsValid) return BadRequest();

            var materialList = await _materialService.GetAllByDesignId(designId);
            if (materialList is null) return NotFound();
            return Ok(materialList);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromForm] UpdateMaterialDTO dto)
        {
            var material = await _materialService.Update(id, dto);
            if (material is null) return NotFound();
            return Ok(material);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var material = await _materialService.Delete(id);
            if (material is null) return NotFound();
            return Ok(material);
        }

    }
}
