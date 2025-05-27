using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Passengers.Application.v1.Commands.Passenger;

public sealed record DeletePassengerCommand(Guid Id) : IRequest<Result>;
