using System.Text.Json;
using System.Text.Json.Serialization;

namespace PlatformService.Helper
{
    public class JsonOptions
    {
        public static JsonSerializerOptions Default => new JsonSerializerOptions()
        {
            Converters = { new JsonStringEnumConverter() }
        };
    }
}
