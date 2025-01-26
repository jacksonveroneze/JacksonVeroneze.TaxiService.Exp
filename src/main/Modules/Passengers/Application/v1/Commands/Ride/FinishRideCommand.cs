using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Passengers.Application.v1.Commands.Ride;

public sealed record FinishRideCommand(Guid Id) : IRequest<Result>;