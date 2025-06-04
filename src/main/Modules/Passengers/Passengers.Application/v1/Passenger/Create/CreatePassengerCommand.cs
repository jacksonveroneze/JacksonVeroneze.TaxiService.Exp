using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseDomain.Enums;

namespace TaxiService.Modules.Passengers.Application.v1.Passenger.Create;

public sealed record CreatePassengerCommand
    : IRequest<Result<CreatePassengerCommandResponse>>
{
    public string? FullName { get; init; }

    public DateOnly? Birthday { get; init; }

    public Gender? Gender { get; init; }

    public string? Document { get; init; }

    public string? Email { get; init; }
}
