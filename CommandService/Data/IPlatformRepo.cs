using CommandService.Models;

namespace CommandService.Data
{
    public interface IPlatformRepo
    {
        IEnumerable<Platform> GetPlatforms();

        Platform? GetPlatformById(int platformId);

        void CreatePlatform(Platform platform);

        bool PlatformExists(int platformId);

        bool SaveChanges();

        bool DoesExternalPlatformExists(int externalId);
    }
}
