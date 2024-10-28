using JacksonVeroneze.NET.Result;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.Position;

public sealed record AddPositionCommand : IRequest<Result>
{
    [JsonPropertyName("ride_id")]
    public Guid RideId { get; init; }

    [JsonPropertyName("latitude")]
    public float Latitude { get; init; }

    [JsonPropertyName("longitude")]
    public float Longitude { get; init; }
}