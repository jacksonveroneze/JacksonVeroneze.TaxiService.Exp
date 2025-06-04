using System.Linq.Expressions;

using TaxiService.Common.BaseInfrastructure;
using TaxiService.Modules.Drivers.Application.v1.Driver;
using TaxiService.Modules.Drivers.Application.v1.Driver.Common.Filters;
using TaxiService.Modules.Drivers.Application.v1.Driver.Common.Models;

namespace TaxiService.Modules.Drivers.Infrastructure.Builders.Filters;

public class DriverFilterBuilder(
    DriverFilter filter)
{
    public static DriverFilterBuilder Create(
        DriverFilter filter)
    {
        return new DriverFilterBuilder(filter);
    }

    public Expression<Func<DriverDto, bool>> Build()
    {
        FilterBuilder<DriverDto> builder = new();

        if (filter.Ids?.Count != 0)
        {
            builder.And(d => filter.Ids!.Contains(d.Id!.Value));
        }

        return builder.Build();
    }
}
