using JacksonVeroneze.NET.Result;

using TaxiService.Modules.Rides.Application.v1.Ride.GetById;

namespace TaxiService.Modules.Rides.Application.v1.Queries.Ride;

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
