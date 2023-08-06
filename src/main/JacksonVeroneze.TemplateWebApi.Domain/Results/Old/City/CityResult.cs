using System.Text.Json.Serialization;

namespace JacksonVeroneze.TemplateWebApi.Domain.Results.Old.City;

public record CityResult
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("nome")]
    public string? Name { get; init; }
}
