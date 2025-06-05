using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Ride.Application.v1.Ride.Finish;

public sealed record FinishRideCommand(Guid Id) : IRequest<Result>;
