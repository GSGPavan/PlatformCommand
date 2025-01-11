using CommandService.Data;
using CommandService.Models;

namespace CommandService.Server
{
    public class CommandServer : ICommandServer
    {
        private readonly ICommandRepo _commandRepo;
        private readonly IPlatformServer _platformServer;

        public CommandServer(ICommandRepo commandRepo, IPlatformServer platformServer)
        {
            _commandRepo = commandRepo;
            _platformServer = platformServer;
        }

        public void CreateCommandForPlatform(int platformId, Command command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            command.PlatformId = platformId;

            _commandRepo.CreateCommand(command);
            _commandRepo.SaveChanges();
        }

        public Command? GetCommandForPlatform(int platformId, int commandId)
        {
            return _commandRepo.GetCommandForPlatform(platformId, commandId);
        }

        public IEnumerable<Command> GetCommandsForPlatform(int platformId)
        {
            if(!_platformServer.PlatformExists(platformId))
            {
                throw new Exception("Invalid Platform");
            }

            return _commandRepo.GetCommandsForPlatform(platformId);
        }
    }
}
