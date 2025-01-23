using CommandService.Server;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace CommandService.AsyncComm
{
    public class MessageBusSubscriber : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IEventProcessor _eventProcessor;
        private readonly ILogger<MessageBusSubscriber> _logger;
        private IChannel? _channel;

        public MessageBusSubscriber(IConfiguration configuration, IConnection connection, IEventProcessor eventProcessor,
            ILogger<MessageBusSubscriber> logger)
        {
            _configuration = configuration;
            _connection = connection;
            _eventProcessor = eventProcessor;
            _logger = logger;
        }

        private async Task Initialize()
        {
            if (_channel == null)
            {
                _channel = await _connection.CreateChannelAsync();

                await _channel.ExchangeDeclareAsync(_configuration["RabbitMq:ExchangeName"]!,
                    _configuration["RabbitMq:ExchangeType"]!, durable: true);

                await _channel.QueueDeclareAsync(_configuration["RabbitMq:QueueName"]!, durable: true);

                await _channel.QueueBindAsync(_configuration["RabbitMq:QueueName"]!, 
                    _configuration["RabbitMq:ExchangeName"]!, _configuration["RabbitMq:RoutingKey"]!);

                _logger.LogInformation("Channel is created with a fanout exchange and queue");
            }
        }

        protected override async Task<Task> ExecuteAsync(CancellationToken stoppingToken)
        {
            await Initialize();

            var consumer = new AsyncEventingBasicConsumer(_channel!);

            consumer.ReceivedAsync += (a, b) =>
            {
                _logger.LogInformation("A message received from the queue");

                var body = b.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());
                _eventProcessor.ProcessEvent(message);

                return Task.CompletedTask;
            };

            await _channel!.BasicConsumeAsync(_configuration["RabbitMq:QueueName"]!, autoAck: true, consumer);

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            if (_channel != null && _channel.IsOpen)
            {
                _channel.CloseAsync();
            }

            base.Dispose();
        }
    }
}
