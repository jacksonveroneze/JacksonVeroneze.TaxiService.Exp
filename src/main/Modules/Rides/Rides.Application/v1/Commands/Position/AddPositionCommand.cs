using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Rides.Application.v1.Commands.Position;

public sealed record AddPositionCommand
    : IRequest<Result>
{
    public Guid RideId { get; init; }

    public float Latitude { get; init; }

    public float Longitude { get; init; }
}
