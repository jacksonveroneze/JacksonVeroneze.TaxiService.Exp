using JacksonVeroneze.NET.Result;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.Ride;

public sealed record CancelRideCommand(Guid Id) : IRequest<Result>;