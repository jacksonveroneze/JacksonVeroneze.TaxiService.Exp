using JacksonVeroneze.TaxiService.Exp.Domain.Entities;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Transaction;

public interface ITransactionReadRepository
{
    Task<TransactionEntity?> GetByRideIdAsync(
        Guid rideId,
        CancellationToken cancellationToken = default);
}