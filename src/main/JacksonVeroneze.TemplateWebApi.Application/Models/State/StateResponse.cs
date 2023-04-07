namespace JacksonVeroneze.TemplateWebApi.Application.Models.State;

public record StateResponse
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }
}