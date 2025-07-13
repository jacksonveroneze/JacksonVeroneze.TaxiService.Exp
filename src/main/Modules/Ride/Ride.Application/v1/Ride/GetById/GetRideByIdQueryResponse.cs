using TaxiService.Common.BaseApplication.Models.Common;
using TaxiService.Modules.Ride.Application.v1.Ride.Common.Models;

namespace TaxiService.Modules.Ride.Application.v1.Ride.GetById;

public sealed record GetRideByIdQueryResponse :
    DataResponse<RideResponse>;
