using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Payment.Application.v1.Payment.Request;

public sealed record RequestCommand
    : IRequest<Result>
{
    public Guid RideId { get; init; }
}
