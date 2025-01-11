using CommandService.Data;
using CommandService.Models;

namespace CommandService.Server
{
    public class PlatformServer : IPlatformServer
    {
        private readonly IPlatformRepo _platformRepo;

        public PlatformServer(IPlatformRepo platformRepo)
        {
            _platformRepo = platformRepo;
        }

        public void CreatePlatform(Platform platform)
        {
            if (platform == null)
            {
                throw new ArgumentNullException(nameof(platform));
            }

            _platformRepo.CreatePlatform(platform);
        }

        public Platform? GetPlatformById(int platformId)
        {
            return _platformRepo.GetPlatformById(platformId);
        }

        public IEnumerable<Platform> GetPlatforms()
        {
            return _platformRepo.GetPlatforms();
        }

        public bool PlatformExists(int platformId)
        {
            return _platformRepo.PlatformExists(platformId);
        }
    }
}
