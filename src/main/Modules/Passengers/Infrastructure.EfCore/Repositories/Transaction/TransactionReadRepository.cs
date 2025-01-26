using JacksonVeroneze.NET.EntityFramework.Interfaces;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Transaction;
using TaxiService.Modules.Passengers.Domain.Entities;
using TaxiService.Modules.Passengers.Domain.Specifications.Transaction;

namespace TaxiService.Modules.Passengers.Infrastructure.EfCore.Repositories.Transaction;

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