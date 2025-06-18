using Backend_.Net.DTO;
using Backend_.Net.Entities;

namespace Backend_.Net.Service
{
    public interface IColorService
    {
        Task<Color?> AddColorAsync(ColorDTO request);
        Task<List<Color>> GetAllColorsAsync();
        Task<List<Color>> GetColorsByDesignIdAsync(Guid designId);
        Task<Color?> GetColorByIdAsync(Guid id);
        Task<Color?> UpdateColorAsync(Guid id, ColorDTO request);
        Task<bool> DeleteColorAsync(Guid id);
    }
}
