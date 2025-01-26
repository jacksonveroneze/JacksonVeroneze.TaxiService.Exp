using JacksonVeroneze.NET.EntityFramework.Interfaces;
using TaxiService.Modules.Rides.Application.v1.Interfaces.Repositories.Transaction;
using TaxiService.Modules.Rides.Domain.Entities;

namespace TaxiService.Modules.Rides.Infrastructure.EfCore.Repositories.Transaction;

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