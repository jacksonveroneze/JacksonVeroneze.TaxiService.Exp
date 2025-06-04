using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Payments.Application.v1.Commands.Payment;

public sealed record RequestCommand
    : IRequest<Result>
{
    public Guid RideId { get; init; }
}
