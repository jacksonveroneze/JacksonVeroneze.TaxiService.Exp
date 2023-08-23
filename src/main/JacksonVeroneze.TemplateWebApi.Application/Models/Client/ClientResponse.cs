namespace JacksonVeroneze.TemplateWebApi.Application.Models.Client;

public sealed record ClientResponse
{
    [JsonPropertyName("id")]
    public Guid? Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("email")]
    public string? Email { get; init; }
}
