using Backend_.Net.Data;
using Backend_.Net.DTO;
using Backend_.Net.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend_.Net.Service
{
    public class DesignConceptService : IDesignConceptService
    {
        private readonly AppDbContext _context;

        public DesignConceptService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DesignConcept?> AddDesignConceptAsync(DesignConceptDTO request)
        {
            var concept = new DesignConcept
            {
                Id = Guid.NewGuid(),
                DesignId = request.DesignId,
                Concept = request.Concept
            };

            _context.DesignConcepts.Add(concept);
            await _context.SaveChangesAsync();

            return concept;
        }

        public async Task<bool> DeleteDesignConceptAsync(Guid id)
        {
            var concept = await _context.DesignConcepts.FindAsync(id);
            if (concept == null)
                return false;

            _context.DesignConcepts.Remove(concept);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<DesignConcept>?> GetDesignConceptByDesignIdAsync(Guid designId)
        {
            return await _context.DesignConcepts
                .Where(dc => dc.DesignId == designId)
                .ToListAsync();
        }

        public async Task<DesignConcept?> GetDesignConceptByIdAsync(Guid id)
        {
            return await _context.DesignConcepts
                .FirstOrDefaultAsync(dc => dc.Id == id);
        }

        public async Task<DesignConcept?> UpdateDesignConceptAsync(Guid id, DesignConceptDTO request)
        {
            var concept = await _context.DesignConcepts.FindAsync(id);
            if (concept == null)
                return null;

            concept.Concept = request.Concept;
            
            _context.DesignConcepts.Update(concept);
            await _context.SaveChangesAsync();

            return concept;
        }
    }
}
