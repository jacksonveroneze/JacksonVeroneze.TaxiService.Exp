namespace JacksonVeroneze.TemplateWebApi.Application.Models.Bank;

public struct BankResponse
{
    [JsonPropertyName("id")]
    public Guid? Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }
}
