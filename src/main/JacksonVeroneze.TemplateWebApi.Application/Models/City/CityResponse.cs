namespace JacksonVeroneze.TemplateWebApi.Application.Models.City;

public struct CityResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }
}