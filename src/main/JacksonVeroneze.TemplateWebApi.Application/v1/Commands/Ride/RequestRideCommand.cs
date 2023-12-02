using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Ride;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Commands.Ride;

public sealed record RequestRideCommand :
    IRequest<Result<RequestRideCommandResponse>>
{
    [JsonPropertyName("user_id")]
    public Guid UserId { get; init; }

    [JsonPropertyName("latitude_from")]
    public float LatitudeFrom { get; init; }

    [JsonPropertyName("longitude_from")]
    public float LongitudeFrom { get; init; }

    [JsonPropertyName("latitude_to")]
    public float LatitudeTo { get; init; }

    [JsonPropertyName("longitude_to")]
    public float LongitudeTo { get; init; }
}
