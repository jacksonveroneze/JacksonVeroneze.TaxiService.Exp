using JacksonVeroneze.TemplateWebApi.Domain.Enums;

namespace JacksonVeroneze.TemplateWebApi.Application.Models.Bank;

public class BankResponse
{
    [JsonPropertyName("id")]
    public Guid? Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("status")]
    public BankStatus? Status { get; init; }
}
