using System.Text.Json;
using System.Text.Json.Serialization;

namespace Boilerplate.Utils.Json
{
    public static class JsonConfiguration
    {
        public static JsonSerializerOptions ConfigureJsonOptions(JsonSerializerOptions options)
        {
            if (options == null) options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
            options.Converters.Add(new TimeSpanConverter());
            options.Converters.Add(new JsonStringEnumConverter());
            return options;
        }

        public static JsonSerializerOptions GetJsonConfiguration()
        {
            return ConfigureJsonOptions(new JsonSerializerOptions(JsonSerializerDefaults.Web));
        }
    }
}
