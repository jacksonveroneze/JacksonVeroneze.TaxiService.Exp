using System.Linq.Expressions;

using TaxiService.Common.BaseInfrastructure;
using TaxiService.Modules.Passengers.Application.v1.Models.Passenger;
using TaxiService.Modules.Passengers.Domain.Filters;

namespace TaxiService.Modules.Passengers.Infrastructure.Builders.Filters;

public class PassengerFilterBuilder(PassengerFilter filter)
{
    private readonly PassengerFilter _filter =
        filter ?? throw new ArgumentNullException(nameof(filter));

    public static PassengerFilterBuilder Create(PassengerFilter filter)
    {
        return new PassengerFilterBuilder(filter);
    }

    public Expression<Func<PassengerDto, bool>> Build()
    {
        FilterBuilder<PassengerDto> builder = new();

        if (_filter.Ids?.Count != 0)
        {
            builder.And(d => _filter.Ids!.Contains(d.Id!.Value));
        }

        return builder.Build();
    }
}
