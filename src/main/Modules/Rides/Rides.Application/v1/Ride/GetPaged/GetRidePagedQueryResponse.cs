using TaxiService.Common.BaseApplication.Models.Base.Pagination.Cursor;
using TaxiService.Modules.Rides.Application.v1.Ride.Common.Models;

namespace TaxiService.Modules.Rides.Application.v1.Ride.GetPaged;

public sealed record GetRidePagedQueryResponse
    : PagedResponse<RideResponse>;
