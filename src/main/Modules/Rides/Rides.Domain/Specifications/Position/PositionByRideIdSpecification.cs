using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using TaxiService.Common.BaseDomain.Specifications.Base;
using TaxiService.Modules.Rides.Domain.Entities;

namespace TaxiService.Modules.Rides.Domain.Specifications.Position;

[ExcludeFromCodeCoverage]
public class PositionByRideIdSpecification(
    Guid rideId) : BaseSpecification<PositionEntity>
{
    public override Expression<Func<PositionEntity, bool>> ToExpression()
    {
        return spec => spec.RideId == rideId;
    }
}