using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using CommandsService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CommandsService.Controllers
{
    [Route("api/com/platforms/{platformId}/[controller]")]
    [ApiController]
    public class CommadsController : ControllerBase
    {
        private readonly ICommandRepo _repository;
        private readonly IMapper _mapper;

        public CommadsController(ICommandRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommandReadDto>>> GetCommandsForPlatform(int platformId)
        {
            if (!(await _repository.PlatformExistAsync(platformId)))
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(await _repository.GetCommandsForPlatformAsync(platformId)));
        }

        [HttpGet("{commandId}", Name = "GetCommandForPlatform")]
        public async Task<ActionResult<CommandReadDto>> GetCommandForPlatform(int platformId, int commandId)
        {
            if (!(await _repository.PlatformExistAsync(platformId)))
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CommandReadDto>(await _repository.GetCommandAsync(platformId, commandId)));
        }

        [HttpPost]
        public async Task<ActionResult<CommandCreateDto>> CreateCommandForPlatform(int platformId, CommandCreateDto commandDto)
        {
            if (!(await _repository.PlatformExistAsync(platformId)))
            {
                return NotFound();
            }

            var command = _mapper.Map<Command>(commandDto);
            await _repository.CreateCommandAsync(platformId, command);
            await _repository.SaveChangesAsync();

            var resultDto = _mapper.Map<CommandReadDto>(command);
            return CreatedAtAction(nameof(GetCommandForPlatform), new { platformId = resultDto.PlatformId, commandId = resultDto.Id }, resultDto);
        }

    }
}
