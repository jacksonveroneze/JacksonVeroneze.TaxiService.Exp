using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Transaction;

public interface ITransactionReadRepository
{
    public Task<TransactionEntity?> GetByRideIdAsync(
        Guid rideId,
        CancellationToken cancellationToken = default);
}