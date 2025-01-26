using TaxiService.Modules.Rides.Application.Models.Base;

namespace TaxiService.Modules.Rides.Application.v1.Models.Ride;

public sealed record GetRideByIdQueryResponse :
    DataResponse<RideResponse>;