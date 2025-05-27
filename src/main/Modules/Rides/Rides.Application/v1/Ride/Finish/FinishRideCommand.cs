using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Rides.Application.v1.Ride.Finish;

public sealed record FinishRideCommand(Guid Id) : IRequest<Result>;
