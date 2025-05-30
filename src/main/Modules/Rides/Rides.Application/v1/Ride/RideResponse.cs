using TaxiService.Common.BaseApplication.Models.Models.Common;
using TaxiService.Modules.Drivers.PublicApi.v1.Models;
using TaxiService.Modules.Passengers.PublicApi.v1.Models;
using TaxiService.Modules.Rides.Domain.Enums;

namespace TaxiService.Modules.Rides.Application.v1.Ride;

public sealed record RideResponse
{
    public Guid? Id { get; init; }

    public Guid? PassengerId { get; init; }

    public Guid? DriverId { get; init; }

    public decimal? Fare { get; init; }

    public double? Distance { get; init; }

    public CoordinateResponse? From { get; init; }

    public CoordinateResponse? To { get; init; }

    public RideStatus? Status { get; init; }
    
    public PassengerPublicApiDto? Passenger { get; set; }

    public DriverPublicApiDto? Driver { get; set; }
}
