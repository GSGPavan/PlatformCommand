using CommandService.Models;

namespace CommandService.Server
{
    public interface IPlatformServer
    {
        IEnumerable<Platform> GetPlatforms();

        Platform? GetPlatformById(int platformId);

        void CreatePlatform(Platform platform);

        bool PlatformExists(int platformId);
    }
}
