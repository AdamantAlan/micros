using PlatformService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformService.Data
{
    public class PlatformRepo : IPlatformRepo
    {
        private readonly AppDbContext _context;

        public PlatformRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreatPlatformAsync(Platform p)
        {
            if (p == null)
                throw new ArgumentNullException(nameof(p));
            await _context.Platforms.AddAsync(p);
        }

        public Task<IEnumerable<Platform>> GetAllPlatformsAsync()
        {
            return Task.Run(() => { return _context.Platforms.AsEnumerable(); });
        }

        public Task<Platform> GetPlatformByIdAsync(int id)
        {
            return Task.Run(() => { return _context.Platforms.FirstOrDefault(p => p.Id == id); });
        }

        public async Task<bool> SaveChangeAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
