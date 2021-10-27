using CommandsService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CommandsService.Data
{
    public interface ICommandRepo
    {
        Task<bool> SaveChangesAsync();

        Task<IEnumerable<Platform>> GetAllPlatformsAsync();

        Task CreatePlatformAsync(Platform plat);

        Task<bool> PlatformExistAsync(int platformId);

        Task<IEnumerable<Command>> GetCommandsForPlatformAsync(int platformId);

        Task<Command> GetCommandAsync(int platformId, int commandId);

        Task CreateCommandAsync(int platformId, Command command);

        Task<bool> ExternalPlatformIdExistAsync(int externalId);
    }
}
