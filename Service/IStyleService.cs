using Backend_.Net.DTO;
using Backend_.Net.Entities;

namespace Backend_.Net.Service
{
    public interface IStyleService
    {
        Task<Style> AddStyleAsync(StyleDTO request);
        Task<List<Style>> GetAllStyleAsync();
        Task<Style?> GetStyleByIdAsync(Guid id);
        Task<Style?> UpdateStyleAsync(Guid id, StyleDTO request);
        Task<bool> DeleteStyleAsync(Guid id);
    }
}
