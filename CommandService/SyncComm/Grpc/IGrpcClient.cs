using CommandService.Models;

namespace CommandService.SyncComm.Grpc
{
    public interface IGrpcClient
    {
        List<Platform> GetAllExternalPlatforms();
    }
}
