using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Passenger.Application.v1.Passenger.Delete;

public sealed record DeletePassengerCommand(Guid Id) : IRequest<Result>;
