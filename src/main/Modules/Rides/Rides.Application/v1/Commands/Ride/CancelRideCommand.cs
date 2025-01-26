using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Rides.Application.v1.Commands.Ride;

public sealed record CancelRideCommand(Guid Id) : IRequest<Result>;