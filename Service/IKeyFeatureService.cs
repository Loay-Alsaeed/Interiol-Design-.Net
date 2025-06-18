using Backend_.Net.DTO;
using Backend_.Net.Entities;

namespace Backend_.Net.Service
{
    public interface IKeyFeatureService
    {
        Task<KeyFeature?> AddKeyFeatureAsync(KeyFeatureDTO request);
        Task<List<KeyFeature>> GetAllKeyFeatureAsync();
        Task<List<KeyFeature>> GetAllKeyFeatureByDesignIdAsync(Guid designId);
        Task<KeyFeature?> GetKeyFeatureByIdAsync(Guid keyId);
        Task<KeyFeature?> UpdateKeyFeatureAsync(Guid keyId, KeyFeatureDTO request);
        Task<bool> DeleteKeyFeatureAsync(Guid keyId);
    }
}
