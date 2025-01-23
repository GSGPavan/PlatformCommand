using System.Text.Json;
using System.Text.Json.Serialization;

namespace CommandService.Helper
{
    public class JsonOptions
    {
        public static JsonSerializerOptions Default => new JsonSerializerOptions()
        {
            Converters = {new JsonStringEnumConverter()}
        };
    }
}
