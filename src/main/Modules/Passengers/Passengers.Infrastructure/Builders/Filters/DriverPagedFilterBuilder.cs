using System.Linq.Expressions;

using TaxiService.Common.BaseInfrastructure;
using TaxiService.Modules.Passengers.Application.v1.Models.Passenger;
using TaxiService.Modules.Passengers.Domain.Filters;

namespace TaxiService.Modules.Passengers.Infrastructure.Builders.Filters;

public class PassengerPagedFilterBuilder(PassengerPagedFilter filter)
{
    private readonly PassengerPagedFilter _filter =
        filter ?? throw new ArgumentNullException(nameof(filter));

    public static PassengerFilterBuilder Create(
        PassengerPagedFilter filter)
    {
        return new PassengerFilterBuilder(filter);
    }

    public Expression<Func<PassengerDto, bool>> Build()
    {
        FilterBuilder<PassengerDto> builder = new();

        if (_filter.Status.HasValue)
        {
            builder.And(d => d.Status == _filter.Status);
        }

        return builder.Build();
    }
}
