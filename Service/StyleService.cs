using Azure.Core;
using Backend_.Net.Data;
using Backend_.Net.DTO;
using Backend_.Net.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend_.Net.Service
{
    public class StyleService : IStyleService
    {
        private readonly AppDbContext _context;

        public StyleService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Style> AddStyleAsync(StyleDTO request)
        {
            var style = new Style { Title = request.Title };
            _context.Styles.Add(style);
            await _context.SaveChangesAsync();
            return style;
        }

        public async Task<bool> DeleteStyleAsync(Guid id)
        {
            var style = await _context.Styles.FindAsync(id);
            if (style is null) return false;

            _context.Styles.Remove(style);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Style>> GetAllStyleAsync()
        {
            return await _context.Styles.ToListAsync();
        }

        public async Task<Style?> GetStyleByIdAsync(Guid id)
        {
            return await _context.Styles.FindAsync(id);
        }

        public async Task<Style?> UpdateStyleAsync(Guid id, StyleDTO request)
        {
            var style = await _context.Styles.FindAsync(id);
            if (style is null)
                return null;

            style.Title = request.Title;
            _context.Styles.Update(style);
            await _context.SaveChangesAsync();
            return style;
        }
    }
}
