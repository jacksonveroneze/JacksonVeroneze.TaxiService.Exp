namespace TaxiService.Modules.Passengers.Application.v1.Models.Common;

public record CoordinateResponse
{
    public float? Latitude { get; init; }

    public float? Longitude { get; init; }
}