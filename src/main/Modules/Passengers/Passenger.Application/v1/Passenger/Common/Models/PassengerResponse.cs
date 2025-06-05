using TaxiService.Common.BaseDomain.Enums;
using TaxiService.Modules.Passenger.Domain.Enums;

namespace TaxiService.Modules.Passenger.Application.v1.Passenger.Common.Models;

public sealed record PassengerResponse
{
    public Guid? Id { get; init; }

    public string? FullName { get; init; }

    public string? Document { get; init; }

    public DateOnly? Birthday { get; init; }

    public Gender? Gender { get; init; }

    public PassengerStatus? Status { get; init; }
}
