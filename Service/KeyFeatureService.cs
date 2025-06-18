using Backend_.Net.Data;
using Backend_.Net.DTO;
using Backend_.Net.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend_.Net.Service
{
    public class KeyFeatureService : IKeyFeatureService
    {

        private readonly AppDbContext _context;

        public KeyFeatureService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<KeyFeature?> AddKeyFeatureAsync(KeyFeatureDTO request)
        {
            if (request is null) return null;

            var keyFeature = new KeyFeature
            {
                Content = request.Content,
                DesignId = request.DesignId
            };
            await _context.KeyFeatures.AddAsync(keyFeature);
            await _context.SaveChangesAsync();
            return keyFeature;
        }

        public async Task<bool> DeleteKeyFeatureAsync(Guid keyId)
        {
            var keyFeature = await _context.KeyFeatures.FindAsync(keyId);
            if (keyFeature is null) return false;

            _context.KeyFeatures.Remove(keyFeature);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<KeyFeature>> GetAllKeyFeatureAsync()
        {
            return await _context.KeyFeatures.ToListAsync();
        }

        public async Task<List<KeyFeature>> GetAllKeyFeatureByDesignIdAsync(Guid designId)
        {
            return await _context.KeyFeatures
           .Where(c => c.DesignId == designId)
           .ToListAsync();
        }

        public async Task<KeyFeature?> GetKeyFeatureByIdAsync(Guid keyId)
        {
            return await _context.KeyFeatures.FindAsync(keyId);
        }

        public async Task<KeyFeature?> UpdateKeyFeatureAsync(Guid keyId, KeyFeatureDTO request)
        {
            var key = await _context.KeyFeatures.FindAsync(keyId);

            if (key is null) return null;

            key.Content = request.Content;

            _context.KeyFeatures.Update(key);
            await _context.SaveChangesAsync();
            return key;
        }
    }
}
