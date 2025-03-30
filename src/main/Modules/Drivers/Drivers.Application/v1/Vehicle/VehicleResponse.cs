using TaxiService.Modules.Drivers.Domain.Enums;

namespace TaxiService.Modules.Drivers.Application.v1.Vehicle;

public sealed record VehicleResponse
{
    public Guid? Id { get; init; }

    public Guid DriverId { get; init; }

    public string? Model { get; init; }

    public string? Brand { get; init; }

    public string? Plate { get; init; }

    public string? Color { get; init; }

    public int? Year { get; init; }

    public VehicleStatus? Status { get; init; }
}