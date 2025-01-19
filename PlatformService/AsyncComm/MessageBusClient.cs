﻿using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace PlatformService.AsyncComm
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _rabbitMqConnection;
        private IChannel? _channel;

        public MessageBusClient(IConfiguration configuration, IConnection rabbitMqConnection) 
        {
            _configuration = configuration;
            _rabbitMqConnection = rabbitMqConnection;
        }

        private async Task CreateChannel()
        {
            if(_channel == null)
            {
                _channel = await _rabbitMqConnection.CreateChannelAsync();
            }
        }

        public async Task<bool> SendMessage<T>(T message, string exchangeName, string exchangeType, string routingKey)
        {
            if (message == null || exchangeName == null || exchangeType == null || routingKey == null)
            {
                throw new Exception("Argument should not be null");
            }

            bool isSent = false;

            await CreateChannel();

            if (_channel != null)
            {
                await _channel.ExchangeDeclareAsync(exchangeName, exchangeType, durable: true);

                string messageString = JsonSerializer.Serialize<T>(message);
                byte[] body = Encoding.UTF8.GetBytes(messageString);

                await _channel.BasicPublishAsync(exchangeName, routingKey, body);

                isSent = true;
            }

            return isSent;
        }
    }
}
