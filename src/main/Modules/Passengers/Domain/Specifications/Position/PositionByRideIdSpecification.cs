using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using TaxiService.Modules.Passengers.Domain.Entities;
using TaxiService.Modules.Passengers.Domain.Specifications.Base;

namespace TaxiService.Modules.Passengers.Domain.Specifications.Position;

[ExcludeFromCodeCoverage]
public class PositionByRideIdSpecification(
    Guid rideId) : BaseSpecification<PositionEntity>
{
    public override Expression<Func<PositionEntity, bool>> ToExpression()
    {
        return spec => spec.RideId == rideId;
    }
}