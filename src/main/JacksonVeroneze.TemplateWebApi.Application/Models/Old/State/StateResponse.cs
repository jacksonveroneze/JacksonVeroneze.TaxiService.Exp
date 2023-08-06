namespace JacksonVeroneze.TemplateWebApi.Application.Models.Old.State;

public struct StateResponse
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }
}
