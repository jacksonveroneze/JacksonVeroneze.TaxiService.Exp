using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Ride.Application.v1.Position.Add;

public sealed record AddPositionCommand
    : IRequest<Result>
{
    public Guid RideId { get; init; }

    public float Latitude { get; init; }

    public float Longitude { get; init; }
}
