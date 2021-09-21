using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
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
        public PlatformsController(IPlatformRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
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
            return CreatedAtRoute(nameof(GetPlatformById), new { id = plat.Id }, responce);
        }
    }
}
