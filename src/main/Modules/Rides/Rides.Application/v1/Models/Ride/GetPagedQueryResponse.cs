using TaxiService.Common.BaseApplication.Models.Base.Pagination.Cursor;

namespace TaxiService.Modules.Rides.Application.v1.Models.Ride;

public sealed record GetPagedQueryResponse
    : PagedResponse<RideResponse>;