using CommandsService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CommandsService.Data
{
    interface ICommandRepo
    {
        Task<bool> SaveChanges();

        Task<IEnumerable<Platform>> GetAllPlatforms();

        Task CreatePlatform(Platform plat);

        Task<bool> PlatformExist(int platformId);

        Task<IEnumerable<Command>> GetCommandsForPlatform(int platformId);

        Task<Command> GetCommand(int platformId, int commandId);

        Task CreateCommand(int platformId, Command command);
    }
}
