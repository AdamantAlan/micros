using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CommandsService.Controllers
{
    [Route("api/com/[Controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly ICommandRepo _repository;
        private readonly IMapper _mapper;

        public PlatformsController(ICommandRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlatformReadDto>>> GetAllPlatforms() =>
            Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(
                await _repository.GetAllPlatformsAsync())
                );

        [HttpPost]
        public ActionResult TestConnection()
        {
            return Ok("Access to Seerver");
        }
    }
}
