namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Transaction;

public sealed record TransactionResponse
{
    [JsonPropertyName("id")]
    public Guid? Id { get; init; }
}