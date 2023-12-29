using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Specifications.Base;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Specifications.Transaction;

[ExcludeFromCodeCoverage]
public class TransactionByRideIdSpecification(
    Guid rideId) : BaseSpecification<TransactionEntity>
{
    public override Expression<Func<TransactionEntity, bool>> ToExpression()
    {
        return spec => spec.RideId == rideId;
    }
}