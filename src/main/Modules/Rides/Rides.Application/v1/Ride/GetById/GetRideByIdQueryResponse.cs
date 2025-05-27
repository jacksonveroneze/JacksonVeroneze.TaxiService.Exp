using TaxiService.Common.BaseApplication.Models.Base;

namespace TaxiService.Modules.Rides.Application.v1.Ride.GetById;

public sealed record GetRideByIdQueryResponse :
    DataResponse<RideResponse>;
