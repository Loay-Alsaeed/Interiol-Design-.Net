using Backend_.Net.DTO;
using Backend_.Net.Entities;

namespace Backend_.Net.Service
{
    public interface ILayoutImageService
    {
        Task<bool> AddlayoutImageAsync(LayoutImageDTO dto);
        Task<object?> UpdateLayoutImageAsync(LayoutImageDTO dto);
        Task<object> GetLayoutImagesAsync(Guid designId);
        Task<string?> DeleteLayoutImage(Guid designId);
    }
}
