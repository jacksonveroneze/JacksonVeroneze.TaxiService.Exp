using System.Linq.Expressions;

using TaxiService.Common.BaseInfrastructure;
using TaxiService.Modules.Passengers.Application.v1.Passenger;
using TaxiService.Modules.Passengers.Domain.Filters;

namespace TaxiService.Modules.Passengers.Infrastructure.Builders.Filters;

public class PassengerPagedFilterBuilder(
    PassengerPagedFilter filter)
{
    public static PassengerPagedFilterBuilder Create(
        PassengerPagedFilter filter)
    {
        return new PassengerPagedFilterBuilder(filter);
    }

    public Expression<Func<PassengerDto, bool>> Build()
    {
        FilterBuilder<PassengerDto> builder = new();

        if (filter.Status.HasValue)
        {
            builder.And(d => d.Status == filter.Status);
        }

        return builder.Build();
    }
}
