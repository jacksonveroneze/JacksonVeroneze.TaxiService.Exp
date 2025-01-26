using TaxiService.Common.Application.Models.Base.Pagination.Cursor;

namespace TaxiService.Modules.Rides.Application.v1.Models.Ride;

public sealed record GetRidePagedQueryResponse
    : PagedResponse<RideResponse>;