using System.Linq.Expressions;
using TaxiService.Common.BaseInfrastructure;
using TaxiService.Modules.Driver.Application.v1.Vehicle.Common.Filters;
using TaxiService.Modules.Driver.Application.v1.Vehicle.Common.Models;

namespace TaxiService.Modules.Driver.Infrastructure.Builders.Filters;

public class VehicleFilterBuilder(
    VehicleFilter filter)
{
    public static VehicleFilterBuilder Create(
        VehicleFilter filter)
    {
        return new VehicleFilterBuilder(filter);
    }

    public Expression<Func<VehicleDto, bool>> Build()
    {
        FilterBuilder<VehicleDto> builder = new();

        if (filter.Ids?.Count > 0)
        {
            builder.And(d => filter.Ids!.Contains(d.Id));
        }
        
        if (filter.DriverIds?.Count > 0)
        {
            builder.And(d => filter.DriverIds!.Contains(d.DriverId));
        }

        return builder.Build();
    }
}
