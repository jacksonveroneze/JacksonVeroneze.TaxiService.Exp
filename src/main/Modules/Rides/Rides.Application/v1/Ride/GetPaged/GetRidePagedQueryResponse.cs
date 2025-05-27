using TaxiService.Common.BaseApplication.Models.Base.Pagination.Cursor;

namespace TaxiService.Modules.Rides.Application.v1.Ride.GetPaged;

public sealed record GetRidePagedQueryResponse
    : PagedResponse<RideResponse>;
