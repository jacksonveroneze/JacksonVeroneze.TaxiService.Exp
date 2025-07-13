using TaxiService.Common.SharedKernel.Enums;

namespace TaxiService.Modules.Ride.Application.v1.Ride.MatchDrivers;

public record MatchDriversResponse
{
    public required MatchDriversDriverResponse Driver { get; init; }

    public required MatchDriversVehicleResponse Vehicle { get; init; }

    public double Score { get; init; }

    public double DistanceInKm { get; init; }

    public TimeSpan EstimatedArrivalTime { get; init; }
}

public sealed record MatchDriversDriverResponse
{
    public Guid Id { get; init; }

    public required string FullName { get; init; }

    public Gender Gender { get; init; }
}

public sealed record MatchDriversVehicleResponse
{
    public Guid Id { get; init; }

    public string? Model { get; init; }

    public string? Brand { get; init; }

    public string? Plate { get; init; }

    public string? Color { get; init; }

    public int? Year { get; init; }
}
