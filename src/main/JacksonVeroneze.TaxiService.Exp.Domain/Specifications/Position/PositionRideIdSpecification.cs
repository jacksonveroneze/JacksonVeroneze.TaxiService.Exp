using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Specifications.Base;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Specifications.Position;

[ExcludeFromCodeCoverage]
public class PositionRideIdSpecification(
    Guid? rideId) : BaseSpecification<PositionEntity>
{
    private readonly Expression<Func<PositionEntity, bool>> _defaultExpression = _ => true;

    public override Expression<Func<PositionEntity, bool>> ToExpression()
    {
        if (!rideId.HasValue)
        {
            return _defaultExpression;
        }

        return spec => spec.Ride!.Id == rideId;
    }
}
