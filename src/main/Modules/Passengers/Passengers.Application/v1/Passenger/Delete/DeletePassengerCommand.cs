using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Passengers.Application.v1.Passenger.Delete;

public sealed record DeletePassengerCommand(Guid Id) : IRequest<Result>;
