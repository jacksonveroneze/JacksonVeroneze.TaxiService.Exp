using TaxiService.Common.BaseApplication.Models.Models.Common;
using TaxiService.Modules.Drivers.Application.v1.Driver.Common.Models;
using TaxiService.Modules.Passengers.Application.v1.Passenger;
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
    
    public PassengerDto? Passenger { get; set; }

    public DriverDto? Driver { get; set; }
}
