using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Passengers.Application.v1.Models.Passenger;
using TaxiService.Modules.Passengers.Domain.Enums;

namespace TaxiService.Modules.Passengers.Application.v1.Commands.Passenger;

public sealed record CreatePassengerCommand
    : IRequest<Result<CreatePassengerCommandResponse>>
{
    public string? FullName { get; init; }

    public DateOnly? Birthday { get; init; }

    public Gender? Gender { get; init; }

    public string? Document { get; init; }

    public string? Email { get; init; }
}
