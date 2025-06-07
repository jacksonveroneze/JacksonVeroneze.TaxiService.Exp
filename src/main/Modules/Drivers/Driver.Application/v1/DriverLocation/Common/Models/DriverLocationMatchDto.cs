namespace TaxiService.Modules.Driver.Application.v1.DriverLocation.Common.Models;

public sealed record DriverLocationMatchDto
{
    public Guid Id { get; init; }
    
    public Guid DriverId { get; init; }
    
    public double Latitude { get; init; }

    public double Longitude { get; init; }
    
    public double DistanceInMeters { get; init; }
}
