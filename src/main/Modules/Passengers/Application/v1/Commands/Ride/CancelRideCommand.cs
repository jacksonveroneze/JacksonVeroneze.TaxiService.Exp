using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Passengers.Application.v1.Commands.Ride;

public sealed record CancelRideCommand(Guid Id) : IRequest<Result>;