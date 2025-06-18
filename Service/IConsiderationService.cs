using Backend_.Net.DTO;
using Backend_.Net.Entities;

namespace Backend_.Net.Service
{
    public interface IConsiderationService
    {
        Task<Consideration?> AddConsiderationAsync(ConsiderationDTO request);
        Task<List<Consideration>> GetAllConsiderationAsync();
        Task<List<Consideration>> GetAllConsiderationByDesignIdAsync(Guid designId);
        Task<Consideration?> GetConsiderationByIdAsync(Guid ConId);
        Task<Consideration?> UpdateConsiderationAsync(Guid ConId, ConsiderationDTO request);
        Task<bool> DeleteConsiderationAsync(Guid ConId);
    }
}
