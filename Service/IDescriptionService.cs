using Backend_.Net.DTO;
using Backend_.Net.Entities;

namespace Backend_.Net.Service
{
    public interface IDescriptionService
    {
        Task<DesignDescription?> AddDescriptionAsync(DescriptionDTO request);
        Task<List<DesignDescription?>> GetDescriptionBtDesignIdAsync(Guid designId);
        Task<DesignDescription?> GetDescriptionByIdAsync(Guid id);
        Task<DesignDescription?> UpdateDescriptionAsync(Guid id, DescriptionDTO request);
        Task<bool> DeleteDescriptionAsync(Guid id);
    }
}
