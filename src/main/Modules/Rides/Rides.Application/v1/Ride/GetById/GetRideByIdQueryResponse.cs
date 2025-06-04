using TaxiService.Common.BaseApplication.Models.Base;
using TaxiService.Modules.Rides.Application.v1.Ride.Common.Models;

namespace TaxiService.Modules.Rides.Application.v1.Ride.GetById;

public sealed record GetRideByIdQueryResponse :
    DataResponse<RideResponse>;
