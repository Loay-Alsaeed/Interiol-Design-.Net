using Backend_.Net.DTO;
using Backend_.Net.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Backend_.Net.Service
{
    public interface IMaterialService
    {
        Task<Material?> Create([FromForm] MaterialDTO dto);
        Task<List<Material?>> GetAll();
        Task<List<Material?>> GetAllByDesignId(Guid designId);
        Task<Material?> Get(Guid id);
        Task<Material?> Update(Guid id, [FromForm] UpdateMaterialDTO dto);
        Task<string?> Delete(Guid id);
    }
}
