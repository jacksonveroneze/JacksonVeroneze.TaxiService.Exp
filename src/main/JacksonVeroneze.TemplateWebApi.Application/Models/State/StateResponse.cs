namespace JacksonVeroneze.TemplateWebApi.Application.Models.State;

public struct StateResponse
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }
}