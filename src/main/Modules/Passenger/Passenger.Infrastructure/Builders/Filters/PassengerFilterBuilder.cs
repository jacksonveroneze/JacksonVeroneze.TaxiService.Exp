using System.Linq.Expressions;
using TaxiService.Common.SharedKernel.Builders;
using TaxiService.Modules.Passenger.Application.v1.Passenger.Common.Filters;
using TaxiService.Modules.Passenger.Application.v1.Passenger.Common.Models;

namespace TaxiService.Modules.Passenger.Infrastructure.Builders.Filters;

public class PassengerFilterBuilder(
    PassengerFilter filter)
{
    public static PassengerFilterBuilder Create(
        PassengerFilter filter)
    {
        return new PassengerFilterBuilder(filter);
    }

    public Expression<Func<PassengerDto, bool>> Build()
    {
        FilterBuilder<PassengerDto> builder = new();

        if (filter.Ids?.Count != 0)
        {
            builder.And(d => filter.Ids!.Contains(d.Id!.Value));
        }

        return builder.Build();
    }
}
