using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Ride.Application.v1.Ride.Start;

public sealed record StartRideCommand(Guid Id) : IRequest<Result>;
