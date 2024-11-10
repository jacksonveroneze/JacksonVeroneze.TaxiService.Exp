using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Ride;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.Ride;

public sealed record RequestRideCommand : IRequest<Result<RequestRideCommandResponse>>
{
    public Guid UserId { get; init; }

    public float LatitudeFrom { get; init; }

    public float LatitudeTo { get; init; }

    public float LongitudeFrom { get; init; }

    public float LongitudeTo { get; init; }
}