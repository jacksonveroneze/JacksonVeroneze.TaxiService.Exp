namespace TaxiService.Modules.Driver.PublicApi.v1.Models;

public record NearbyDriverDto
{
    public Guid Id { get; init; }
    
    public Guid DriverId { get; init; }
    
    public double Latitude { get; init; }

    public double Longitude { get; init; }
    
    public double DistanceInMeters { get; init; }
}
