using TaxiService.Common.BaseDomain.Enums;
using TaxiService.Modules.Passengers.Domain.Enums;

namespace TaxiService.Modules.Passengers.PublicApi.v1.Models;

public sealed record PassengerPublicApiDto
{
    public Guid Id { get; init; }

    public required string FullName { get; init; }

    public required string Document { get; init; }

    public required string Email { get; init; }

    public DateOnly Birthday { get; init; }

    public Gender Gender { get; init; }

    public PassengerStatus Status { get; init; }
}
