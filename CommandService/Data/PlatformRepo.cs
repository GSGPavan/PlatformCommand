using CommandService.Models;

namespace CommandService.Data
{
    public class PlatformRepo : IPlatformRepo
    {
        private readonly AppDbContext _context;

        public PlatformRepo(AppDbContext context)
        {
            _context = context;
        }

        public void CreatePlatform(Platform platform)
        {
            if (platform == null)
            {
                throw new ArgumentNullException(nameof(platform));
            }

            _context.Platforms.Add(platform);
        }

        public Platform? GetPlatformById(int platformId)
        {
            return _context.Platforms.FirstOrDefault(platform => platform.Id == platformId);
        }

        public IEnumerable<Platform> GetPlatforms()
        {
            return _context.Platforms.ToList();
        }

        public bool PlatformExists(int platformId)
        {
            Platform? platform = GetPlatformById(platformId);

            return platform != null;
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        bool IPlatformRepo.DoesExternalPlatformExists(int externalId)
        {
            return _context.Platforms.Any(platform => platform.ExternalId == externalId);
        }
    }
}
