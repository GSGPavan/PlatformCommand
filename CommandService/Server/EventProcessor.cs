using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using CommandService.Enum;
using CommandService.Helper;
using CommandService.Models;
using System.Text.Json;

namespace CommandService.Server
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IMapper _mapper;
        private readonly ILogger<EventProcessor> _logger;

        public EventProcessor(IServiceScopeFactory serviceScopeFactory, IMapper mapper, ILogger<EventProcessor> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _mapper = mapper;
            _logger = logger;
        }

        public void ProcessEvent(string message)
        {
            EventType eventType = DetermineEvent(message);

            switch(eventType)
            {
                case EventType.PlatformPublished:
                    {
                        AddPlatform(message);
                        break;
                    }
                default:
                    {
                        _logger.LogInformation("Invalid Event type cant be processed");
                        break;
                    }
            }
        }

        private void AddPlatform(string message)
        {
            PlatformPublishedDto? platformPublishedDto = JsonSerializer.Deserialize<PlatformPublishedDto>(message);

            if (platformPublishedDto != null)
            {
                Platform platform = _mapper.Map<Platform>(platformPublishedDto);

                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    IPlatformRepo platformRepo = scope.ServiceProvider.GetRequiredService<IPlatformRepo>();

                    if(!platformRepo.DoesExternalPlatformExists(platform.ExternalId))
                    {
                        platformRepo.CreatePlatform(platform);
                        _logger.LogInformation("platform created successfully");
                    }
                }
            }
            else
            {
                _logger.LogError("Deserializing the message returned null..");
            }
        }

        EventType DetermineEvent(string message)
        {
            EventDto? eventDto = JsonSerializer.Deserialize<EventDto>(message, JsonOptions.Default);

            return eventDto?.Event ?? EventType.None;
        }
    }
}
