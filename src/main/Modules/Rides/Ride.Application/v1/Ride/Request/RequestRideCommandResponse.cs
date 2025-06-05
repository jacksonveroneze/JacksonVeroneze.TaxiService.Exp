using TaxiService.Common.BaseApplication.Models.Base;
using TaxiService.Modules.Ride.Application.v1.Ride.Common.Models;

namespace TaxiService.Modules.Ride.Application.v1.Ride.Request;

public record RequestRideCommandResponse
    : DataResponse<RideResponse>;
