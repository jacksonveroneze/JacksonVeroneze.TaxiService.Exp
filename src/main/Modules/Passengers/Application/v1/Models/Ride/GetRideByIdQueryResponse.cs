using TaxiService.Modules.Passengers.Application.Models.Base;

namespace TaxiService.Modules.Passengers.Application.v1.Models.Ride;

public sealed record GetRideByIdQueryResponse :
    DataResponse<RideResponse>;