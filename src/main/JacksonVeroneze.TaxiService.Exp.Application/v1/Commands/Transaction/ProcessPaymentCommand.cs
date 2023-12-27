using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Base;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.Transaction;

public sealed record ProcessPaymentCommand(Guid RideId) :
    IRequest<Result<VoidResponse>>;