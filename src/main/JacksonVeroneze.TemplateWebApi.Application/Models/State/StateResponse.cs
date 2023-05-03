namespace JacksonVeroneze.TemplateWebApi.Application.Models.State;

public struct StateResponse
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }
}