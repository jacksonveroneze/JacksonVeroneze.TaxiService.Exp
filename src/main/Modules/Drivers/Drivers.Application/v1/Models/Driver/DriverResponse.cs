using TaxiService.Modules.Drivers.Domain.Enums;

namespace TaxiService.Modules.Drivers.Application.v1.Models.Driver;

public sealed record DriverResponse
{
    public Guid? Id { get; init; }

    public string? FullName { get; init; }

    public string? Document { get; init; }

    public DateOnly? Birthday { get; init; }

    public Gender? Gender { get; init; }

    public DriverStatus? Status { get; init; }
}