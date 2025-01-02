using PlatformService.Data;
using PlatformService.Models;

namespace PlatformService.Server
{
    public class PlatformServer : IPlatformServer
    {
        private readonly IPlatformRepo _platformRepo;

        public PlatformServer(IPlatformRepo platformRepo)
        {
            _platformRepo = platformRepo;
        }

        public IEnumerable<Platform> GetPlatforms()
        {
            return _platformRepo.GetPlatforms().ToList();
        }

        public Platform? GetPlatformById(int id)
        {
            return _platformRepo.GetPlatformById(id);
        }

        public void CreatePlatform(Platform platform)
        {
            _platformRepo.CreatePlatform(platform);
            _platformRepo.SaveChanges();
        }
    }
}