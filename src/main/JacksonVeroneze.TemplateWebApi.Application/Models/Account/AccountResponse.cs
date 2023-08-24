namespace JacksonVeroneze.TemplateWebApi.Application.Models.Account;

public sealed record AccountResponse
{
    [JsonPropertyName("id")]
    public Guid? Id { get; init; }
}
