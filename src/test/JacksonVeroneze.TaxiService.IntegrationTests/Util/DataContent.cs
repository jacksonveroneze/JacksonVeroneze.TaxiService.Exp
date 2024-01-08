using System.Text.Json.Serialization;

namespace JacksonVeroneze.TemplateWebApi.IntegrationTests.Util;

public class DataContent<TType>
{
    [JsonPropertyName("data")]
    public TType? Data { get; set; }
}
