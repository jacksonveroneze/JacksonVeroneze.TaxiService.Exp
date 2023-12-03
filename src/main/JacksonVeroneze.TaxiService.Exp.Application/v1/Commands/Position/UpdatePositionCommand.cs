using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Base;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.Position;

public sealed record UpdatePositionCommand :
    IRequest<Result<VoidResponse>>
{
    [JsonPropertyName("ride_id")]
    public Guid RideId { get; init; }

    [JsonPropertyName("latitude")]
    public float Latitude { get; init; }

    [JsonPropertyName("longitude")]
    public float Longitude { get; init; }
}
