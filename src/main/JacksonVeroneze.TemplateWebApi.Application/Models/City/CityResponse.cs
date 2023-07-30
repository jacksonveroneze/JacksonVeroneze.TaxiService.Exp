namespace JacksonVeroneze.TemplateWebApi.Application.Models.City;

public struct CityResponse
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }
}
