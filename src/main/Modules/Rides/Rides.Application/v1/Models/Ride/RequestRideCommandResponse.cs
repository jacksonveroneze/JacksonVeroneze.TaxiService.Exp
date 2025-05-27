using TaxiService.Common.BaseApplication.Models.Base;

namespace TaxiService.Modules.Rides.Application.v1.Models.Ride;

public record RequestRideCommandResponse
    : DataResponse<RideResponse>;
