using JacksonVeroneze.NET.EntityFramework.Interfaces;
using TaxiService.Modules.Rides.Application.v1.Interfaces.Repositories.Transaction;
using TaxiService.Modules.Rides.Domain.Entities;
using TaxiService.Modules.Rides.Domain.Specifications.Transaction;

namespace TaxiService.Modules.Rides.Infrastructure.EfCore.Repositories.Transaction;

[ExcludeFromCodeCoverage]
public class TransactionReadRepository(
    IEfCoreRepository<TransactionEntity> service)
    : ITransactionReadRepository
{
    public Task<TransactionEntity?> GetByRideIdAsync(
        Guid rideId,
        CancellationToken cancellationToken = default)
    {
        TransactionByRideIdSpecification spec = new(rideId);

        return service.GetByIdAsync(spec.ToExpression(), cancellationToken);
    }
}