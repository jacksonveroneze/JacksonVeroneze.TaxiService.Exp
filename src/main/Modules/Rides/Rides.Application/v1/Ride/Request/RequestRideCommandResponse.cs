using TaxiService.Common.BaseApplication.Models.Base;

namespace TaxiService.Modules.Rides.Application.v1.Ride.Request;

public record RequestRideCommandResponse
    : DataResponse<RideResponse>;
