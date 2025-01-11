using AutoMapper;
using CommandService.Dtos;
using CommandService.Models;
using CommandService.Server;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers
{
    [Route("api/c/Plaforms/{platformId}/[controller]")]
    [ApiController]
    public class CommandController : ControllerBase
    {
        private readonly ICommandServer _commandServer;
        private readonly IMapper _mapper;

        public CommandController(ICommandServer commandServer, IMapper mapper)
        {
            _commandServer = commandServer;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetCommandsForPlatform(int platformId)
        {
            IEnumerable<Command> commands = _commandServer.GetCommandsForPlatform(platformId);

            IEnumerable<CommandReadDto> commandReadDtos = _mapper.Map<IEnumerable<CommandReadDto>>(commands);

            return Ok(commandReadDtos);
        }

        [HttpGet("{commandId}")]
        public ActionResult<IEnumerable<CommandReadDto>> GetCommandForPlatform(int platformId, int commandId)
        {
            Command? command = _commandServer.GetCommandForPlatform(platformId, commandId);

            if (command == null)
            {
                return NotFound();
            }

            CommandReadDto commandReadDto = _mapper.Map<CommandReadDto>(command);

            return Ok(commandReadDto);
        }

        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommandForPlatform(int platformId, CommandCreateDto commandCreateDto)
        {
            if (commandCreateDto == null || platformId == 0)
            {
                return BadRequest();
            }

            Command command = _mapper.Map<Command>(commandCreateDto);

            _commandServer.CreateCommandForPlatform(platformId, command);

            CommandReadDto commandReadDto = _mapper.Map<CommandReadDto>(command);

            return Ok(commandReadDto);
        }
    }
}
