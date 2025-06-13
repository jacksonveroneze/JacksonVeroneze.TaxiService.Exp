using TaxiService.Common.BaseDomain.Enums;
using TaxiService.Modules.Driver.Domain.Enums;

namespace TaxiService.Modules.Driver.Application.v1.Driver.Common.Models;

public sealed record DriverResponse
{
    public Guid Id { get; init; }

    public required string FullName { get; init; }

    public required string Document { get; init; }

    public DateOnly Birthday { get; init; }

    public Gender Gender { get; init; }

    public DriverStatus Status { get; init; }

    public DriverAvailabilityResponse? Availability { get; set; }
}

public sealed record DriverAvailabilityResponse
{
    public DriverAvailabilityStatus Status { get; init; }
}
