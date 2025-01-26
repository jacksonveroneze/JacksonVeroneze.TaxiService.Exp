using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Transaction;

public interface ITransactionWriteRepository
{
    public Task CreateAsync(
        TransactionEntity entity,
        CancellationToken cancellationToken = default);
}