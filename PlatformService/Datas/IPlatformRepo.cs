using PlatformService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformService.Data
{
    public interface IPlatformRepo
    {
        Task<bool> SaveChangeAsync();

        Task<IEnumerable<Platform>> GetAllPlatformsAsync();

        Task<Platform> GetPlatformByIdAsync(int id);

        Task CreatPlatformAsync(Platform p);
    }
}
