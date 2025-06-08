using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Ride.Application.v1.Ride.Cancel;

public sealed record CancelRideCommand(Guid Id) : IRequest<Result>;
