using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Rides.Application.v1.Ride.Request;

public sealed record RequestRideCommand
    : IRequest<Result<RequestRideCommandResponse>>
{
    public Guid PassengerId { get; init; }

    public float LatitudeFrom { get; init; }

    public float LatitudeTo { get; init; }

    public float LongitudeFrom { get; init; }

    public float LongitudeTo { get; init; }
}
