using Backend_.Net.Data;
using Backend_.Net.DTO;
using Backend_.Net.Entities;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace Backend_.Net.Service
{
    public class LayoutImageService : ILayoutImageService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public LayoutImageService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<bool> AddlayoutImageAsync(LayoutImageDTO dto)
        {
            if (dto.Image == null || dto.Image.Length == 0)
                return false;

            // إنشاء اسم فريد للصورة
            var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(dto.Image.FileName)}";
            var imagePath = Path.Combine(_env.WebRootPath, "images/layoutImages", uniqueFileName);

            // حفظ الصورة على السيرفر
            Directory.CreateDirectory(Path.GetDirectoryName(imagePath)!); // يتأكد أن المجلد موجود
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                await dto.Image.CopyToAsync(stream);
            }

            //var image = new LayoutImage
            //{
            //    ImageUrl = imagePath,
            //    DesignId = dto.DesignId,
            //};

            var design = await _context.Designs.FindAsync(dto.DesignId);
            if (design is null) return false;

            design.LayoutImageUrl = $"images/layoutImages/{uniqueFileName}";


            _context.Designs.Update(design);

            //_context.LayoutImages.Add(image);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<string?> DeleteLayoutImage(Guid designId)
        {
            var design = await _context.Designs.FindAsync(designId);
            if (design is null) return null;

            var oldImagePath = Path.Combine(_env.WebRootPath, design.LayoutImageUrl.TrimStart('/'));
            if (System.IO.File.Exists(oldImagePath))
                System.IO.File.Delete(oldImagePath);

            design.LayoutImageUrl = String.Empty;

            _context.Designs.Update(design);
            await _context.SaveChangesAsync();
            return "deleted Successfully";
        }

        public async Task<object> GetLayoutImagesAsync(Guid designId)
        {
            var design = await _context.Designs.FindAsync(designId);
            if (design is null) return null;

            var layoutImages = new LayoutImage
            {
                ImageUrl = design.LayoutImageUrl,
                DesignId = designId,
            };
            var obj = new GetLayoutImageUrlDTO
            {
                DesignId = designId,
                ImageUrl = design.LayoutImageUrl,
            };

            return obj;
        }

        public async Task<object?> UpdateLayoutImageAsync(LayoutImageDTO dto)
        {
            var design = await _context.Designs.FindAsync(dto.DesignId);
            if (design == null) return null;

            if (dto.Image == null || dto.Image.Length == 0)
                return null;

            var oldImagePath = Path.Combine(_env.WebRootPath, design.LayoutImageUrl.TrimStart('/'));
            if (System.IO.File.Exists(oldImagePath))
                System.IO.File.Delete(oldImagePath);

            var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(dto.Image.FileName)}";
            var imagePath = Path.Combine(_env.WebRootPath, "images/layoutImages", uniqueFileName);

            // حفظ الصورة على السيرفر
            Directory.CreateDirectory(Path.GetDirectoryName(imagePath)!); // يتأكد أن المجلد موجود
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                await dto.Image.CopyToAsync(stream);
            }

            design.LayoutImageUrl = $"images/layoutImages/{uniqueFileName}";

            _context.Designs.Update(design);
            await _context.SaveChangesAsync();

            return design;
        
        }
    }
}
