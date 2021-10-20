using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.AsyncDataService;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _repo;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandDataClient;
        internal IMessageBusClient _bus;

        public PlatformsController(IPlatformRepo repo, IMapper mapper, ICommandDataClient commandDataClient, IMessageBusClient bus)
        {
            _repo = repo;
            _mapper = mapper;
            _commandDataClient = commandDataClient;
            _bus = bus;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlatformReadDto>>> GetAllPlatform()
        {
            var plts = await _repo.GetAllPlatformsAsync();
            return Ok(_mapper.Map<IEnumerable<Platform>>(plts));
        }

        [HttpGet("{id}", Name = "GetPlatformById")]
        public async Task<ActionResult<PlatformReadDto>> GetPlatformById(int id)
        {
            var plts = await _repo.GetPlatformByIdAsync(id);
            return Ok(_mapper.Map<Platform>(plts));
        }

        [HttpPost]
        public async Task<ActionResult<PlatformReadDto>> CreatePlatform(PlatformCreateDto platform)
        {
            var plat = _mapper.Map<Platform>(platform);
            await _repo.CreatPlatformAsync(plat);
            await _repo.SaveChangeAsync();
            var responce = _mapper.Map<PlatformReadDto>(plat);

            //send sync message
            try
            {
                await _commandDataClient.SendPlatformToCommand(responce);
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR {e.Message}");
            }

            //send async message
            try
            {
                var platformBusDto = _mapper.Map<PlatformPublishedDto>(responce);
                platformBusDto.Event = "Platform_Publish";
                _bus.PublishNewPlatform(platformBusDto);
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR {e.Message}");
            }
            return CreatedAtRoute(nameof(GetPlatformById), new { id = plat.Id }, responce);
        }
    }
}
