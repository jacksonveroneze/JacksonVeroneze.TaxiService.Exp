using System.Linq.Expressions;
using TaxiService.Common.SharedKernel.Builders;
using TaxiService.Modules.Passenger.Application.v1.Passenger.Common.Filters;
using TaxiService.Modules.Passenger.Application.v1.Passenger.Common.Models;

namespace TaxiService.Modules.Passenger.Infrastructure.Builders.Filters;

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
