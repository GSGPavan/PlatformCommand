namespace PlatformService.SyncComm.Http
{
    public interface IApiClient
    {
        Task<TResponse?> GetHttpResponse<TResponse>(string url, HttpMethod httpMethod, 
            object? request = null);
    }
}