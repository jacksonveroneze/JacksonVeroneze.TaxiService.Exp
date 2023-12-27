using JacksonVeroneze.TaxiService.Exp.Domain.Entities;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Transaction;

public interface ITransactionWriteRepository
{
    Task CreateAsync(
        TransactionEntity entity,
        CancellationToken cancellationToken = default);
}