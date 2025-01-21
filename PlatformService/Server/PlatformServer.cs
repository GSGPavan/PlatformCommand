using AutoMapper;
using PlatformService.AsyncComm;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Enum;
using PlatformService.Models;
using PlatformService.SyncComm.Http;

namespace PlatformService.Server
{
    public class PlatformServer : IPlatformServer
    {
        private readonly IConfiguration _configuration;
        private readonly IPlatformRepo _platformRepo;
        private readonly IApiClient _apiClient;
        private readonly IMessageBusClient _messageBusClient;
        private readonly IMapper _mapper;
        private readonly ILogger<PlatformServer> _logger;

        public PlatformServer(IConfiguration configuration, IPlatformRepo platformRepo, IApiClient apiClient, 
            IMessageBusClient messageBusClient, IMapper mapper, ILogger<PlatformServer> logger)
        {
            _configuration = configuration;
            _platformRepo = platformRepo;
            _apiClient = apiClient;
            _messageBusClient = messageBusClient;
            _mapper = mapper;
            _logger = logger;
        }

        public IEnumerable<Platform> GetPlatforms()
        {
            return _platformRepo.GetPlatforms().ToList();
        }

        public Platform? GetPlatformById(int id)
        {
            return _platformRepo.GetPlatformById(id);
        }

        public async Task CreatePlatform(Platform platform)
        {
            _platformRepo.CreatePlatform(platform);
            _platformRepo.SaveChanges();

            try
            {
                string? s = await _apiClient.GetHttpResponse<string>($"{_configuration["CommandServiceBaseUrl"]}api/c/Platform", 
                    HttpMethod.Post);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Cannot call command service due to {ex.Message}");
            }

            PlatformPublishedDto platformPublishedDto = _mapper.Map<PlatformPublishedDto>(platform);
            platformPublishedDto.Event = EventType.PlatformPublished;

            try
            {
                await _messageBusClient.SendMessage<PlatformPublishedDto>(platformPublishedDto,
                    _configuration["PlatformPublished:ExchangeName"]!,
                    _configuration["PlatformPublished:ExchangeType"]!,
                    _configuration["PlatformPublished:RoutingKey"]!);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Cannot send Platform due to {ex.Message}");
            }
            
        }
    }
}