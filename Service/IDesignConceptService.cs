using Backend_.Net.DTO;
using Backend_.Net.Entities;

namespace Backend_.Net.Service
{
    public interface IDesignConceptService
    {
        Task<DesignConcept?> AddDesignConceptAsync(DesignConceptDTO request);
        Task<List<DesignConcept>?> GetDesignConceptByDesignIdAsync(Guid designId);
        Task<DesignConcept?> GetDesignConceptByIdAsync(Guid id);
        Task<DesignConcept?> UpdateDesignConceptAsync(Guid id, DesignConceptDTO request);
        Task<bool> DeleteDesignConceptAsync(Guid id);
    }
}
