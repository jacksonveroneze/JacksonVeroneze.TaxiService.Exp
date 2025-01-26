namespace TaxiService.Modules.Rides.Application.v1.Models.Common;

public record CoordinateResponse
{
    public float? Latitude { get; init; }

    public float? Longitude { get; init; }
}