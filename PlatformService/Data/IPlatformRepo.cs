using PlatformService.Models;

namespace PlatformService.Data
{
    public interface IPlatformRepo
    {
        public IEnumerable<Platform> GetPlatforms();

        public Platform? GetPlatformById(int id);

        public void CreatePlatform(Platform platform);

        public bool SaveChanges();
    }
}
