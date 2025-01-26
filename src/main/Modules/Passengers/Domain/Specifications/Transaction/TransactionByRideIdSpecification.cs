using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using TaxiService.Modules.Passengers.Domain.Entities;
using TaxiService.Modules.Passengers.Domain.Specifications.Base;

namespace TaxiService.Modules.Passengers.Domain.Specifications.Transaction;

[ExcludeFromCodeCoverage]
public class TransactionByRideIdSpecification(
    Guid rideId) : BaseSpecification<TransactionEntity>
{
    public override Expression<Func<TransactionEntity, bool>> ToExpression()
    {
        return spec => spec.RideId == rideId;
    }
}