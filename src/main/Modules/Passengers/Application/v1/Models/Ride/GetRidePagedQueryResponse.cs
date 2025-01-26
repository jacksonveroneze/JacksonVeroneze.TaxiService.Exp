using TaxiService.Modules.Passengers.Application.Models.Base.Pagination.Cursor;

namespace TaxiService.Modules.Passengers.Application.v1.Models.Ride;

public sealed record GetRidePagedQueryResponse
    : PagedResponse<RideResponse>;