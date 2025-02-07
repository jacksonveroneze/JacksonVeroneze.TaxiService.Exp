using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Rides.Application.v1.Commands.Ride;

public sealed record StartCommand(Guid Id) : IRequest<Result>;