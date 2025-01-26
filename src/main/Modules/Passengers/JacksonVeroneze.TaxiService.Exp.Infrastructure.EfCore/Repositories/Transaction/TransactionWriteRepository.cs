using JacksonVeroneze.NET.EntityFramework.Interfaces;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Transaction;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.Repositories.Transaction;

[ExcludeFromCodeCoverage]
public class TransactionWriteRepository(
    IEfCoreRepository<TransactionEntity> service)
    : ITransactionWriteRepository
{
    public async Task CreateAsync(TransactionEntity entity,
        CancellationToken cancellationToken = default)
    {
        await service.CreateAsync(entity, cancellationToken);

        await service.UnitOfWork.CommitAsync(cancellationToken);
    }
}