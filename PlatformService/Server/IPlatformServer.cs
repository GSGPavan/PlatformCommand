using PlatformService.Models;

namespace PlatformService.Server
{
    public interface IPlatformServer
    {
        IEnumerable<Platform> GetPlatforms();

        Platform? GetPlatformById(int id);

        void CreatePlatform(Platform platform);
    }
}