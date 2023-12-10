using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Specifications.Base;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Specifications.Transaction;

[ExcludeFromCodeCoverage]
public class TransactionRideIdSpecification(
    Guid? rideId) : BaseSpecification<TransactionEntity>
{
    private readonly Expression<Func<TransactionEntity, bool>> _defaultExpression = _ => true;

    public override Expression<Func<TransactionEntity, bool>> ToExpression()
    {
        if (!rideId.HasValue)
        {
            return _defaultExpression;
        }

        return spec => spec.Ride!.Id == rideId;
    }
}
