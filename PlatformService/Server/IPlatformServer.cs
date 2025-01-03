using PlatformService.Models;

namespace PlatformService.Server
{
    public interface IPlatformServer
    {
        IEnumerable<Platform> GetPlatforms();

        Platform? GetPlatformById(int id);

        Task CreatePlatform(Platform platform);
    }
}