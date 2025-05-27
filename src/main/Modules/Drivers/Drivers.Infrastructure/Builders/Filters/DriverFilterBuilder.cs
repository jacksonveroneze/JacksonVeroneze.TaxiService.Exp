using System.Linq.Expressions;

using TaxiService.Common.BaseInfrastructure;
using TaxiService.Modules.Drivers.Application.v1.Driver.Common.Models;
using TaxiService.Modules.Drivers.Domain.Filters;

namespace TaxiService.Modules.Drivers.Infrastructure.Builders.Filters;

public class DriverFilterBuilder(DriverFilter filter)
{
    private readonly DriverFilter _filter =
        filter ?? throw new ArgumentNullException(nameof(filter));

    public static DriverFilterBuilder Create(DriverFilter filter)
    {
        return new DriverFilterBuilder(filter);
    }

    public Expression<Func<DriverDto, bool>> Build()
    {
        FilterBuilder<DriverDto> builder = new();

        if (_filter.Ids?.Count != 0)
        {
            builder.And(d => _filter.Ids!.Contains(d.Id!.Value));
        }

        return builder.Build();
    }
}
