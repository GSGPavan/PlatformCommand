using CommandService.Models;

namespace CommandService.Data
{
    public class CommandRepo : ICommandRepo
    {
        private readonly AppDbContext _context;

        public CommandRepo(AppDbContext context)
        {
            _context = context;
        }

        public void CreateCommand(Command command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            if(command.PlatformId == 0)
            {
                throw new Exception("Command should have PlatformId");
            }

            _context.Commands.Add(command);
        }

        public Command? GetCommandForPlatform(int platformId, int commandId)
        {
            return _context.Commands.FirstOrDefault(command => command.PlatformId == platformId && command.Id == commandId);
        }

        public IEnumerable<Command> GetCommandsForPlatform(int platformId)
        {
            return _context.Commands.Where(Command => Command.PlatformId == platformId).ToList();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
