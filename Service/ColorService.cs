using System.ComponentModel.Design;
using Backend_.Net.Data;
using Backend_.Net.DTO;
using Backend_.Net.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend_.Net.Service
{
    public class ColorService : IColorService
    {
        private readonly AppDbContext _context;
        public ColorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Color?> AddColorAsync(ColorDTO request)
        {
            var color = new Color
            {
                DesignId = request.DesignId,
                Name = request.Name,
                ColorNumber = request.ColorNumber,
                application = request.application,
            };

            var design = await _context.Designs.FindAsync(color.DesignId);
            if (design is null) return null;

            //var designColor = new DesignColor
            //{
            //    DesignId = designId,
            //    Color = color,
            //    ColorId = color.Id,
            //};

            await _context.Colors.AddAsync(color);
            //await _context.DesignColors.AddAsync(designColor);
            await _context.SaveChangesAsync();
            return color;
        }

        public async Task<bool> DeleteColorAsync(Guid id)
        {
            // var color = await _context.Colors
            // .Include(c => c.DesignColors)
            //.FirstOrDefaultAsync(c => c.Id == id);

            var color = await _context.Colors.FindAsync(id);
            if (color == null) return false;

            // Remove from junction table
            //_context.DesignColors.RemoveRange(color.DesignColors);
            _context.Colors.Remove(color);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Color>> GetColorsByDesignIdAsync(Guid designId)
        {
            //return await _context.DesignColors
            //  .Where(dc => dc.DesignId == designId)
            //  .Select(dc => dc.Color)
            //  .ToListAsync();

            return await _context.Colors.Where(c => c.DesignId == designId).ToListAsync();

        }
        public async Task<List<Color>> GetAllColorsAsync()
        {
            return await _context.Colors.ToListAsync();
        }

        public async Task<Color?> GetColorByIdAsync(Guid id)
        {
            return await _context.Colors.FindAsync(id);
        }

        public async Task<Color?> UpdateColorAsync(Guid id, ColorDTO request)
        {
            var color = await _context.Colors.FindAsync(id);
            if (color is null)
                return null;

            color.Name = request.Name;
            color.ColorNumber = request.ColorNumber;
            color.application = request.application;
            await _context.SaveChangesAsync();
            return color;
        }
    }
}
