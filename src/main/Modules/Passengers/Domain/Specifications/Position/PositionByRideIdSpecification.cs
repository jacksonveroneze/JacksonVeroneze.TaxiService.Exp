using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Specifications.Base;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Specifications.Position;

[ExcludeFromCodeCoverage]
public class PositionByRideIdSpecification(
    Guid rideId) : BaseSpecification<PositionEntity>
{
    public override Expression<Func<PositionEntity, bool>> ToExpression()
    {
        return spec => spec.RideId == rideId;
    }
}