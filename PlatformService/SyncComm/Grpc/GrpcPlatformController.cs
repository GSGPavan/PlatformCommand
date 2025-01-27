using AutoMapper;
using Grpc;
using Grpc.Core;
using PlatformService.Models;
using PlatformService.Server;

namespace PlatformService.SyncComm.Grpc
{
    public class GrpcPlatformController : GrpcPlatformService.GrpcPlatformServiceBase
    {
        private readonly IPlatformServer _platformServer;
        private readonly IMapper _mapper;
        private readonly ILogger<GrpcPlatformController> _logger;

        public GrpcPlatformController(IPlatformServer platformServer, IMapper mapper, ILogger<GrpcPlatformController> logger)
        {
            _platformServer = platformServer;
            _mapper = mapper;
            _logger = logger;
        }

        public override Task<PlatformResponse> GetAllPlatforms(GetAllRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Requested GetAllPlatforms");

            List<Platform> platforms = _platformServer.GetPlatforms().ToList();
            PlatformResponse platformResponse = new PlatformResponse();

            foreach (Platform platform in platforms)
            {
                GrpcPlatform grpcPlatform = _mapper.Map<GrpcPlatform>(platforms);
                platformResponse.Platforms.Add(grpcPlatform);
            }

            return Task.FromResult(platformResponse);
        }
    }
}
