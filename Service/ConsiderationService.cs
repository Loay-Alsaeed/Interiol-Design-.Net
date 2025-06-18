using Backend_.Net.Data;
using Backend_.Net.DTO;
using Backend_.Net.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend_.Net.Service
{
    public class ConsiderationService : IConsiderationService

    {
        private readonly AppDbContext _context;

        public ConsiderationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Consideration?> AddConsiderationAsync(ConsiderationDTO request)
        {
            if (request is null) return null;

            var consideration = new Consideration
            {
                Title = request.Title,
                Description = request.Description,
                DesignId = request.DesignId
            };
            await _context.Considerations.AddAsync(consideration);
            await _context.SaveChangesAsync();
            return consideration;
        }

        public async Task<bool> DeleteConsiderationAsync(Guid ConId)
        {
            var considuration = await _context.Considerations.FindAsync(ConId);
            if (considuration is null) return false;

            _context.Considerations.Remove(considuration);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Consideration>> GetAllConsiderationByDesignIdAsync(Guid designId)
        {
            return await _context.Considerations
            .Where(c => c.DesignId == designId)
            .ToListAsync();
        }
        
        public async Task<List<Consideration>> GetAllConsiderationAsync()
        {
            return await _context.Considerations.ToListAsync();
        }

        public async Task<Consideration?> GetConsiderationByIdAsync(Guid ConId)
        {
            return await _context.Considerations.FindAsync(ConId);
        }

        public async Task<Consideration?> UpdateConsiderationAsync(Guid ConId, ConsiderationDTO request)
        {
            var cons = await _context.Considerations.FindAsync(ConId);

            if (cons is null) return null;

            cons.Title = request.Title;
            cons.Description = request.Description;

            _context.Considerations.Update(cons);
            await _context.SaveChangesAsync();
            return cons;
        }
    }
}
