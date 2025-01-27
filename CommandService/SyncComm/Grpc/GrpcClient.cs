using AutoMapper;
using CommandService.Models;
using Grpc;
using Grpc.Net.Client;

namespace CommandService.SyncComm.Grpc
{
    public class GrpcClient : IGrpcClient
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ILogger<GrpcClient> _logger;

        public GrpcClient(IConfiguration configuration, IMapper mapper, ILogger<GrpcClient> logger) 
        {
            _configuration = configuration;
            _mapper = mapper;
            _logger = logger;
        }

        public List<Platform> GetAllExternalPlatforms()
        {
            _logger.LogInformation("--> Attempting to call GRPC service for external platforms");

            var channel = GrpcChannel.ForAddress(_configuration["Grpc:Address"]!);
            var client = new GrpcPlatformService.GrpcPlatformServiceClient(channel);

            PlatformResponse platformResponse = client.GetAllPlatforms(new GetAllRequest());

            List<Platform> platforms = _mapper.Map<List<Platform>>(platformResponse.Platforms);

            return platforms;
        }
    }
}
