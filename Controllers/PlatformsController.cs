using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _platformRepo;
        private readonly IMapper _mapper;

        public PlatformsController(IPlatformRepo platformRepo, IMapper mapper)
        {
            _platformRepo = platformRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            IEnumerable<Platform> platforms = _platformRepo.GetPlatforms();
            IEnumerable<PlatformReadDto> platformReadDtos = _mapper.Map<IEnumerable<PlatformReadDto>>(platforms);

            return Ok(platformReadDtos);
        }

        [HttpGet("{id}", Name = nameof(GetPlatformById))]
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        {
            Platform? platform = _platformRepo.GetPlatformById(id);

            if (platform == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PlatformReadDto>(platform));
        }

        [Route("CreatePlatform")]
        [HttpPost]
        public ActionResult<PlatformReadDto> CreatePlatform(PlatformCreateDto platformCreateDto)
        {
            Platform platform = _mapper.Map<Platform>(platformCreateDto);

            _platformRepo.CreatePlatform(platform);
            _platformRepo.SaveChanges();

            PlatformReadDto platformReadDto = _mapper.Map<PlatformReadDto>(platform);

            return CreatedAtRoute(nameof(GetPlatformById), new { platform.Id }, platformReadDto);
        }
    }
}
