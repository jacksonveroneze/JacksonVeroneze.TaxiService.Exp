using JacksonVeroneze.NET.Result;

using TaxiService.Modules.Rides.Application.v1.Ride.Common;
using TaxiService.Modules.Rides.Application.v1.Ride.Common.Enums;

namespace TaxiService.Modules.Rides.Application.v1.Ride.GetById;

public sealed record GetRideByIdQuery(Guid Id) :
    IRequest<Result<GetRideByIdQueryResponse>>
{
    public string? Expands { get; init; }

    public HashSet<RideExpands>? Expand
    {
        get
        {
            IEnumerable<RideExpands> expands = Expands?.Split(',', 
                    StringSplitOptions.RemoveEmptyEntries)
                .Select(Enum.Parse<RideExpands>) ?? [];

            return [..expands];
        }
    }
}
