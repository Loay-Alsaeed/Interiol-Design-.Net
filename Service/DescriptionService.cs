using Backend_.Net.Data;
using Backend_.Net.DTO;
using Backend_.Net.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend_.Net.Service
{
    public class DescriptionService : IDescriptionService
    {
        private readonly AppDbContext _context;

        public DescriptionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DesignDescription?> AddDescriptionAsync(DescriptionDTO request)
        {
            var designDescription = new DesignDescription
            {
                Content = request.Content,
                DesignId = request.DesignId
            };

            await _context.DesignDescriptions.AddAsync(designDescription);
            await _context.SaveChangesAsync();
            return designDescription;
        }

        public async Task<bool> DeleteDescriptionAsync(Guid id)
        {
            var description = await _context.DesignDescriptions.FindAsync(id);
            if (description is null) return false;
            _context.DesignDescriptions.Remove(description);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<DesignDescription?>> GetDescriptionBtDesignIdAsync(Guid designId)
        {
            var description = await _context.DesignDescriptions.Where
                (de => de.DesignId == designId).ToListAsync();
            if (description is null) return null;
            return description;
          
              
        }

        public async Task<DesignDescription?> GetDescriptionByIdAsync(Guid id)
        {
            return await _context.DesignDescriptions.FindAsync(id);
        }

        public async Task<DesignDescription?> UpdateDescriptionAsync(Guid id, DescriptionDTO request)
        {
            var description = await _context.DesignDescriptions.FindAsync(id);
            
            if (description is null) return null;
            
            description.Content = request.Content;

            _context.DesignDescriptions.Update(description);
            await _context.SaveChangesAsync();
            return description;
        }
    }
}
