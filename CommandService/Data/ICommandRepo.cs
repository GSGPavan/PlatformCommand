using CommandService.Models;

namespace CommandService.Data
{
    public interface ICommandRepo
    {
        void CreateCommand(Command command);

        IEnumerable<Command> GetCommandsForPlatform(int platformId);

        Command? GetCommandForPlatform(int platformId, int commandId);

        bool SaveChanges();
    }
}
