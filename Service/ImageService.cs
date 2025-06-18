using Backend_.Net.Data;
using Backend_.Net.DTO;
using Backend_.Net.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_.Net.Service
{
    public class ImageService : IImageService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ImageService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<Image?> AddImageAsync([FromForm] ImageDTO dto)
        {
            if (dto.Image == null || dto.Image.Length == 0)
                return null;

            // إنشاء اسم فريد للصورة
            var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(dto.Image.FileName)}";
            var imagePath = Path.Combine(_env.WebRootPath, "images/designImages", uniqueFileName);

            // حفظ الصورة على السيرفر
            Directory.CreateDirectory(Path.GetDirectoryName(imagePath)!); // يتأكد أن المجلد موجود
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                await dto.Image.CopyToAsync(stream);
            }

            var image = new Image
            {
                DesignId = dto.DesignId,
                ImageUrl = $"images/designImages/{uniqueFileName}"
            };
            _context.Images.Add(image);
            await _context.SaveChangesAsync();
            return image;

        }

        public async Task<List<Image?>> GetAllImagesAsync(Guid designId)
        {
            var images = await _context.Images
               .Where(img => img.DesignId == designId)
               .ToListAsync();

            if (images is null) return null;

            return images;
        }

        public async Task<Image?> UpdateImageAsync([FromForm] ImageDTO dto, Guid imageId)
        {
            var image = await _context.Images.FindAsync(imageId);
            if (image == null) return null;

            if (dto.Image == null || dto.Image.Length == 0)
                return null;

            var oldImagePath = Path.Combine(_env.WebRootPath, image.ImageUrl.TrimStart('/'));
            if (System.IO.File.Exists(oldImagePath))
                System.IO.File.Delete(oldImagePath);

            var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(dto.Image.FileName)}";
            var imagePath = Path.Combine(_env.WebRootPath, "images/designImages", uniqueFileName);

            // حفظ الصورة على السيرفر
            Directory.CreateDirectory(Path.GetDirectoryName(imagePath)!); // يتأكد أن المجلد موجود
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                await dto.Image.CopyToAsync(stream);
            }

            image.ImageUrl = $"images/designImages/{uniqueFileName}";
            _context.Update(image);
            await _context.SaveChangesAsync();
            
            return image;
        }

        public async Task<string?> DeleteImage(Guid imageId)
        {
            var image = await _context.Images.FindAsync(imageId);
            if (image is null) return null;
         
            var oldImagePath = Path.Combine(_env.WebRootPath, image.ImageUrl.TrimStart('/'));
            if (System.IO.File.Exists(oldImagePath))
                System.IO.File.Delete(oldImagePath);

            _context.Images.Remove(image);
            await _context.SaveChangesAsync();

            return "removed Successfully";

        }
    }
}
