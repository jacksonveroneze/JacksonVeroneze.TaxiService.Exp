using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Payments.Application.v1.Commands.Payment;

public sealed record RequestPaymentCommand
    : IRequest<Result>
{
    public Guid RideId { get; init; }
}