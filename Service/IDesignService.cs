using Backend_.Net.DTO;
using Backend_.Net.Entities;

namespace Backend_.Net.Service
{
    public interface IDesignService
    {
        Task<Design?> CreateDesignWithDetailsAsync(AddDesignDTO dto);
        Task<Design?> GetDesignByDesignId(Guid designId);
        Task<List<Design>> GetAllDesigns();
        Task<bool> DeleteDesign(Guid designId);
    }
}
