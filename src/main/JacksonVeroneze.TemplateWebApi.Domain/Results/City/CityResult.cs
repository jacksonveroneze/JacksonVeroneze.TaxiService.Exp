using System.Text.Json.Serialization;

namespace JacksonVeroneze.TemplateWebApi.Domain.Results.City;

public record CityResult
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("nome")]
    public int Name { get; set; }
}