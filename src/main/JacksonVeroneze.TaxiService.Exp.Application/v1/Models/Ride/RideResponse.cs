using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Base;
using JacksonVeroneze.TaxiService.Exp.Domain.Enums;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Ride;

public sealed record RideResponse
{
    public Guid? Id { get; init; }

    public Guid? UserId { get; init; }

    public Guid? DriverId { get; init; }

    public decimal? Fare { get; init; }

    public double? Distance { get; init; }

    public CoordinateResponse? From { get; init; }

    public CoordinateResponse? To { get; init; }

    public RideStatus? Status { get; init; }
}