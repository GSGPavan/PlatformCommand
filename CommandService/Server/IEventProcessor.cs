namespace CommandService.Server
{
    public interface IEventProcessor
    {
        void ProcessEvent(string message);
    }
}
