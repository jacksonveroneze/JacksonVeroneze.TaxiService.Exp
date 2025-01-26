using JacksonVeroneze.NET.EntityFramework.Interfaces;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Transaction;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Specifications.Transaction;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.Repositories.Transaction;

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