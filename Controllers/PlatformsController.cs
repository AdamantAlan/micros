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
        public ActionResult<IEnumerable<PlatformReadDto>> GetAllPlatform()
        {
            var plts = _repo.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<Platform>>(plts));
        }

        [HttpGet("{id}", Name = "GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        {
            var plts = _repo.GetPlatformById(id);
            return Ok(_mapper.Map<Platform>(plts));
        }

        [HttpPost]
        public ActionResult<PlatformReadDto> CreatePlatform(PlatformCreateDto platform)
        {
            var plat = _mapper.Map<Platform>(platform);
            _repo.CreatPlatform(plat);
            _repo.SaveChange();
            var responce = _mapper.Map<PlatformReadDto>(plat);
            return CreatedAtRoute(nameof(GetPlatformById), new { id = plat.Id }, responce);
        }
    }
}
