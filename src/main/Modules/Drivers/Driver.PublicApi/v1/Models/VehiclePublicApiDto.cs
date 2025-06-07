namespace TaxiService.Modules.Driver.PublicApi.v1.Models;

public sealed record VehiclePublicApiDto
{
    public Guid Id { get; init; }

    public Guid DriverId { get; init; }

    public string? Model { get; init; }

    public string? Brand { get; init; }

    public string? Plate { get; init; }

    public string? Color { get; init; }

    public int? Year { get; init; }
}
