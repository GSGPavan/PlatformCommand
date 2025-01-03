using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PlatformService.SyncComm.Http
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TResponse?> GetHttpResponse<TResponse>(string url, 
            HttpMethod httpMethod, object? request = null)
        {
            HttpResponseMessage? httpResponse = null;
            TResponse? response;

            if (httpMethod == HttpMethod.Get)
            {
                httpResponse = await _httpClient.GetAsync(url);
            }
            else
            {
                StringContent content = new StringContent(
                    JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

                httpResponse = await _httpClient.PostAsync(url, content);
            }

            string stringResponse = await httpResponse.Content.ReadAsStringAsync();

            string? responseType = httpResponse.Content.Headers.ContentType?.MediaType;

            if(responseType == "text/plain")
            {
                response = (TResponse)(object)stringResponse;
            }
            else
            {
                response = JsonSerializer.Deserialize<TResponse>(stringResponse);
            }

            return response;
        }
    }
}