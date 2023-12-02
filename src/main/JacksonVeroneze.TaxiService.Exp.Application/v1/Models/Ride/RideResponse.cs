using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Base;
using JacksonVeroneze.TaxiService.Exp.Domain.Enums;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Ride;

public sealed record RideResponse
{
    [JsonPropertyName("id")]
    public Guid? Id { get; init; }

    [JsonPropertyName("user_id")]
    public Guid? UserId { get; init; }

    [JsonPropertyName("driver_id")]
    public Guid? DriverId { get; init; }

    [JsonPropertyName("fare")]
    public decimal? Fare { get; init; }

    [JsonPropertyName("distance")]
    public double? Distance { get; init; }

    [JsonPropertyName("from")]
    public CoordinateResponse? From { get; init; }

    [JsonPropertyName("to")]
    public CoordinateResponse? To { get; init; }

    [JsonPropertyName("status")]
    public RideStatus? Status { get; init; }
}
