using System.Linq.Expressions;
using TaxiService.Common.BaseInfrastructure;
using TaxiService.Modules.Ride.Application.v1.Ride.Common.Filters;
using TaxiService.Modules.Ride.Application.v1.Ride.Common.Models;

namespace TaxiService.Modules.Ride.Infrastructure.Builders.Filters;

public class RidePagedFilterBuilder(
    RidePagedFilter filter)
{
    public static RidePagedFilterBuilder Create(
        RidePagedFilter filter)
    {
        return new RidePagedFilterBuilder(filter);
    }

    public Expression<Func<RideDto, bool>> Build()
    {
        FilterBuilder<RideDto> builder = new();

        if (filter.Status.HasValue)
        {
            builder.And(d => d.Status == filter.Status);
        }

        return builder.Build();
    }
}
