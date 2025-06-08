using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Passenger.Application.v1.Passenger.Activate;

public sealed record ActivatePassengerCommand(Guid Id) : IRequest<Result>;
