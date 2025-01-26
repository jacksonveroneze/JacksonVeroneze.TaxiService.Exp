using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using TaxiService.Common.Domain.Specifications.Base;
using TaxiService.Modules.Rides.Domain.Entities;
using TaxiService.Modules.Rides.Domain.Enums;

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