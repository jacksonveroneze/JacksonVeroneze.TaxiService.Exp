using JacksonVeroneze.NET.Result;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.Transaction;

public sealed record ProcessPaymentCommand(Guid RideId) :
    IRequest<Result>;