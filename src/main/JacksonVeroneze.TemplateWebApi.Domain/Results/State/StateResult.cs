using System.Text.Json.Serialization;

namespace JacksonVeroneze.TemplateWebApi.Domain.Results.State;

public struct StateResult
{
    [JsonPropertyName("sigla")]
    public string? Id { get; set; }

    [JsonPropertyName("nome")]
    public string? Name { get; set; }
}