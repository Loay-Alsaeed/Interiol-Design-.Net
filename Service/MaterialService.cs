using Backend_.Net.Data;
using Backend_.Net.DTO;
using Backend_.Net.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_.Net.Service
{
    public class MaterialService : IMaterialService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public MaterialService(AppDbContext context, IWebHostEnvironment env)
        {
            _env = env;
            _context = context;
        }

        public async Task<Material?> Create([FromForm] MaterialDTO dto)
        {
            if (dto.Image == null || dto.Image.Length == 0)
                return null;

            // إنشاء اسم فريد للصورة
            var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(dto.Image.FileName)}";
            var imagePath = Path.Combine(_env.WebRootPath, "images/materials", uniqueFileName);

            // حفظ الصورة على السيرفر
            Directory.CreateDirectory(Path.GetDirectoryName(imagePath)!); // يتأكد أن المجلد موجود
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                await dto.Image.CopyToAsync(stream);
            }

            var material = new Material
            {
                Id = Guid.NewGuid(),
                DesignId = dto.DesignId,
                Name = dto.Name,
                Description = dto.Description,
                ImageUrl = $"images/materials/{uniqueFileName}",
                Application = dto.Application,
                Supplier = dto.Supplier,
                Sustainability = dto.Sustainability,
                Maintenance = dto.Maintenance
            };
            

            var designMaterial = new DesignMaterial
            {
                DesignId = dto.DesignId,
                MaterialId = material.Id,
                Material = material
            };

            _context.Materials.Add(material);
            _context.DesignMaterials.Add(designMaterial);
            await _context.SaveChangesAsync();

            return material;
        }
        public async Task<List<Material?>> GetAll() 
        {
            var materials = await _context.Materials.ToListAsync();
            if (materials is null) return null;
            return materials;
        }
        public async Task<Material?> Get(Guid id)
        {
            var material = await _context.Materials.FindAsync(id);
            if (material == null) return null;
            return material;
        }
        public async Task<List<Material?>> GetAllByDesignId(Guid designId)
        {
            var materialList = await _context.Materials.Where(m => m.DesignId == designId).ToListAsync();

            return materialList;
        }
        public async Task<Material?> Update(Guid id, [FromForm] UpdateMaterialDTO dto)
        {
            var material = await _context.Materials.FindAsync(id);
            if (material == null)
                return null;

            // تحديث الحقول النصية
            material.Name = dto.Name;
            material.Description = dto.Description;
            material.Application = dto.Application;
            material.Supplier = dto.Supplier;
            material.Sustainability = dto.Sustainability;
            material.Maintenance = dto.Maintenance;

            // معالجة الصورة الجديدة (إن وُجدت)
            if (dto.Image != null && dto.Image.Length > 0)
            {
                // مسار الصورة القديمة
                var oldImagePath = Path.Combine(_env.WebRootPath, material.ImageUrl.TrimStart('/'));
                if (System.IO.File.Exists(oldImagePath))
                    System.IO.File.Delete(oldImagePath);

                // حفظ الصورة الجديدة
                var newFileName = $"{Guid.NewGuid()}_{dto.Image.FileName}";
                var newImagePath = Path.Combine(_env.WebRootPath, "images/materials", newFileName);
                using (var stream = new FileStream(newImagePath, FileMode.Create))
                {
                    await dto.Image.CopyToAsync(stream);
                }

                // تحديث اسم الصورة في الداتا بيس
                material.ImageUrl = $"images/materials{newFileName}";
            }

            await _context.SaveChangesAsync();
            return material;

        }
        public async Task<string?> Delete(Guid id)
        {
            var material = await _context.Materials
            .Include(c => c.DesignMaterial)
           .FirstOrDefaultAsync(c => c.Id == id);
            if (material == null) return null;

            var oldImagePath = Path.Combine(_env.WebRootPath, material.ImageUrl.TrimStart('/'));
            if (System.IO.File.Exists(oldImagePath))
                System.IO.File.Delete(oldImagePath);

            _context.DesignMaterials.RemoveRange(material.DesignMaterial);
            _context.Materials.Remove(material);
            await _context.SaveChangesAsync();
            return "deleted successfully";
        }
    }
}
