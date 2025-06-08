using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Passenger.Application.v1.Passenger.Inactivate;

public sealed record InactivatePassengerCommand(Guid Id) : IRequest<Result>;
