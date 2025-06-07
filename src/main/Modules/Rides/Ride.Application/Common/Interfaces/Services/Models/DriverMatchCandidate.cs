using TaxiService.Modules.Driver.PublicApi.v1.Models;

namespace TaxiService.Modules.Ride.Application.Common.Interfaces.Services.Models;

public record DriverMatchCandidate
{
    public required DriverPublicApiDto Driver { get; init; }
    public required VehiclePublicApiDto Vehicle { get; init; }
    public double Score { get; init; }
    public double DistanceInKm { get; init; }
    public TimeSpan EstimatedArrivalTime { get; init; }
}
