using Backend_.Net.DTO;
using Backend_.Net.Entities;
namespace Backend_.Net.Service
{
    public interface IImageService
    {
        Task<Image?> AddImageAsync(ImageDTO dto);
        Task<Image?> UpdateImageAsync(ImageDTO dto, Guid imageId);
        Task<List<Image?>> GetAllImagesAsync(Guid designId);
        Task<string?> DeleteImage(Guid imageId);
    }
}
