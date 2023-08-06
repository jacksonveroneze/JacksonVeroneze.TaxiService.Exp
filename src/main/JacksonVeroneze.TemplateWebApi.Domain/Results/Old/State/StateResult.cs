using System.Text.Json.Serialization;

namespace JacksonVeroneze.TemplateWebApi.Domain.Results.Old.State;

public record StateResult
{
    [JsonPropertyName("sigla")]
    public string? Id { get; init; }

    [JsonPropertyName("nome")]
    public string? Name { get; init; }
}
