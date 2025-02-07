using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Rides.Application.v1.Commands.Ride;

public sealed record FinishCommand(Guid Id) : IRequest<Result>;