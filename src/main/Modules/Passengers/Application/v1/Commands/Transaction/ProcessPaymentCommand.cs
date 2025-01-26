using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Passengers.Application.v1.Commands.Transaction;

public sealed record ProcessPaymentCommand(Guid RideId) :
    IRequest<Result>;