namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Models;

public record CoordinateResponse
{
    public float? Latitude { get; init; }

    public float? Longitude { get; init; }
}