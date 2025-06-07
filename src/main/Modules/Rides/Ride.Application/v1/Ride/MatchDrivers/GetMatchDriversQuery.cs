using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Ride.Application.v1.Ride.MatchDrivers;

public sealed record GetMatchDriversQuery :
    IRequest<Result<GetMatchDriversQueryResponse>>
{
    public Guid DriverId { get; init; }
}
