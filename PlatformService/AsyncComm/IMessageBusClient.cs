namespace PlatformService.AsyncComm
{
    public interface IMessageBusClient
    {
        Task<bool> SendMessage<T>(T message, string exchangeName, string exchangeType, string routingKey);
    }
}
