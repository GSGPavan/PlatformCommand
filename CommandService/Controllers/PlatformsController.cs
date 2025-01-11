using AutoMapper;
using CommandService.Dtos;
using CommandService.Models;
using CommandService.Server;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformServer _platformServer;
        private readonly IMapper _mapper;

        public PlatformsController(IPlatformServer platformServer, IMapper mapper)
        {
            _platformServer = platformServer;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            IEnumerable<Platform> platforms = _platformServer.GetPlatforms();

            IEnumerable<PlatformReadDto> platformReadDtos = _mapper.Map<IEnumerable<PlatformReadDto>>(platforms);

            return Ok(platformReadDtos);
        }

        [HttpGet("{platformId}")]
        public ActionResult<PlatformReadDto> GetPlatformById(int platformId)
        {
            Platform? platform = _platformServer.GetPlatformById(platformId);

            if (platform == null)
            {
                return NotFound();
            }

            PlatformReadDto platformReadDto = _mapper.Map<PlatformReadDto>(platform);

            return Ok(platformReadDto);
        }

        [HttpPost]
        public ActionResult<string> TestInBoundConnection()
        {
            Console.WriteLine("Testing inbound connection to command service");

            return Ok("Testing inbound connection to command service");
        }

    }
}
