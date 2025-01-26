using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using TaxiService.Modules.Rides.Domain.Entities;
using TaxiService.Modules.Rides.Domain.Enums;
using TaxiService.Modules.Rides.Domain.Specifications.Base;

namespace TaxiService.Modules.Rides.Domain.Specifications.Ride;

[ExcludeFromCodeCoverage]
public class RideActivePerUserSpecification : BaseSpecification<RideEntity>
{
    public override Expression<Func<RideEntity, bool>> ToExpression()
    {
        RideStatus[] status =
        [
            RideStatus.Requested,
            RideStatus.Accepted,
            RideStatus.InProgress
        ];

        return spec => status.Contains(spec.Status);
    }
}