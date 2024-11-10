namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Base;

public record CoordinateResponse
{
    public float? Latitude { get; init; }

    public float? Longitude { get; init; }
}