using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Rides.Application.v1.Models.Ride;

namespace TaxiService.Modules.Rides.Application.v1.Commands.Ride;

public sealed record RequestCommand
    : IRequest<Result<RequestCommandResponse>>
{
    public Guid UserId { get; init; }

    public float LatitudeFrom { get; init; }

    public float LatitudeTo { get; init; }

    public float LongitudeFrom { get; init; }

    public float LongitudeTo { get; init; }
}