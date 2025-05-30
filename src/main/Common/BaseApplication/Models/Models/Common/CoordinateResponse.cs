namespace TaxiService.Common.BaseApplication.Models.Models.Common;

public record CoordinateResponse
{
    public float Latitude { get; init; }

    public float Longitude { get; init; }
}
