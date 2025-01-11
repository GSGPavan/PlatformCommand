using CommandService.Models;

namespace CommandService.Server
{
    public interface ICommandServer
    {
        void CreateCommandForPlatform(int platformId, Command command);

        IEnumerable<Command> GetCommandsForPlatform(int platformId);

        Command? GetCommandForPlatform(int platformId, int commandId);
    }
}
