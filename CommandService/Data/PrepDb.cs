using CommandService.Models;
using CommandService.Server;
using CommandService.SyncComm.Grpc;

namespace CommandService.Data
{
    public class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                IGrpcClient grpcClient = scope.ServiceProvider.GetService<IGrpcClient>()!;
                IPlatformServer platformServer = scope.ServiceProvider.GetService<IPlatformServer>()!;

                SeedExternalPlatorms(grpcClient, platformServer);
            }

        }

        private static void SeedExternalPlatorms(IGrpcClient grpcClient, IPlatformServer platformServer)
        {
            List<Platform> platforms = grpcClient.GetAllExternalPlatforms();

            foreach (Platform platform in platforms)
            {
                if(!platformServer.DoesExternalPlatformExists(platform.ExternalId))
                {
                    platformServer.CreatePlatform(platform);
                }
            }
        }
    }
}
