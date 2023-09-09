using System.Text.Json;
using System.Text.Json.Serialization;

namespace JacksonVeroneze.TemplateWebApi.IntegrationTests.Util;

public static class JsonSerializerConfig
{
    public static JsonSerializerOptions Options =>
        new()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = false,
            PropertyNameCaseInsensitive = false,
            Converters = { new JsonStringEnumConverter() }
        };
}
