using System.Linq.Expressions;
using TaxiService.Common.SharedKernel.Builders;
using TaxiService.Modules.Driver.Application.v1.Driver.Common.Filters;
using TaxiService.Modules.Driver.Application.v1.Driver.Common.Models;

namespace TaxiService.Modules.Driver.Infrastructure.Builders.Filters;

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

        if (filter.Ids?.Count > 0)
        {
            builder.And(d => filter.Ids!.Contains(d.Id!.Value));
        }

        if (filter.Status.HasValue)
        {
            builder.And(d => filter.Status!.Value == filter.Status.Value);
        }

        return builder.Build();
    }
}
