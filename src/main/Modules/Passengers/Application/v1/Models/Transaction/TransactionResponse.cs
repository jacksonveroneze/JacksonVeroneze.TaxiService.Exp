namespace TaxiService.Modules.Passengers.Application.v1.Models.Transaction;

public sealed record TransactionResponse
{
    public Guid? Id { get; init; }
}