namespace TaxiService.Modules.Ride.Application.Common.Interfaces.Services.Models;

public record RideMatchFilter
{
    public Guid PassengerId { get; init; }

    public float Latitude { get; init; }

    public float Longitude { get; init; }
}
