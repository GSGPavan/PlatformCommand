using PlatformService.Data;
using PlatformService.Models;
using PlatformService.SyncComm.Http;

namespace PlatformService.Server
{
    public class PlatformServer : IPlatformServer
    {
        private readonly IConfiguration _configuration;
        private readonly IPlatformRepo _platformRepo;
        private readonly IApiClient _apiClient;

        public PlatformServer(IConfiguration configuration, IPlatformRepo platformRepo, IApiClient apiClient)
        {
            _configuration = configuration;
            _platformRepo = platformRepo;
            _apiClient = apiClient;
        }

        public IEnumerable<Platform> GetPlatforms()
        {
            return _platformRepo.GetPlatforms().ToList();
        }

        public Platform? GetPlatformById(int id)
        {
            return _platformRepo.GetPlatformById(id);
        }

        public async Task CreatePlatform(Platform platform)
        {
            _platformRepo.CreatePlatform(platform);
            _platformRepo.SaveChanges();

            try
            {
                string? s = await _apiClient.GetHttpResponse<string>($"{_configuration["CommandServiceBaseUrl"]}api/c/Platform", 
                    HttpMethod.Post);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Cannot call command service due to {ex.Message}");
            }
        }
    }
}