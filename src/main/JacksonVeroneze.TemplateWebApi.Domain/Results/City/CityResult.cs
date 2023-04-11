using System.Text.Json.Serialization;

namespace JacksonVeroneze.TemplateWebApi.Domain.Results.City;

public struct CityResult
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("nome")]
    public string? Name { get; set; }
}