using CommandsService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandsService.Data
{
    public class CommandRepo : ICommandRepo
    {
        private readonly AppDbContext _context;

        public CommandRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateCommandAsync(int platformId, Command command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }
            command.PlatformId = platformId;
            await _context.Commands.AddAsync(command);
        }

        public async Task CreatePlatformAsync(Platform plat)
        {
            if (plat == null)
            {
                throw new ArgumentNullException(nameof(plat));
            }
            await _context.Platforms.AddAsync(plat);
        }

        public async Task<Command> GetCommandAsync(int platformId, int commandId) => await Task.Run(() =>
        {
            return _context.Commands.Where(c => c.PlatformId == platformId && c.Id == commandId).FirstOrDefault();
        });

        public async Task<IEnumerable<Command>> GetCommandsForPlatformAsync(int platformId) => await Task.Run(() =>
        {
            return _context.Commands.Where(c => c.PlatformId == platformId).OrderBy(c => c.Platform.Name);
        });

        public async Task<IEnumerable<Platform>> GetAllPlatformsAsync() => await _context.Platforms.ToListAsync();

        public async Task<bool> PlatformExistAsync(int platformId) => await Task.Run(() =>
        {
            return _context.Platforms.Any(p => p.Id == platformId);
        });

        public async Task<bool> SaveChangesAsync() => (await _context.SaveChangesAsync()) > 0;

        public async Task<bool> ExternalPlatformIdExistAsync(int externalId) => await Task.Run(() =>
        {
            return _context.Platforms.Any(p => p.ExternalId == externalId);
        });
    }
}
