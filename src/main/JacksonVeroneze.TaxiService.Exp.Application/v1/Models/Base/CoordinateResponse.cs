namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Base;

public record CoordinateResponse
{
    [JsonPropertyName("latitude")]
    public float? Latitude { get; init; }

    [JsonPropertyName("Longitude")]
    public float? Longitude { get; init; }
}
