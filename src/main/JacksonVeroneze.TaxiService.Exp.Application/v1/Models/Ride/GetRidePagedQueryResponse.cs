using JacksonVeroneze.TaxiService.Exp.Application.Models.Base.Pagination.Cursor;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Ride;

public sealed record GetRidePagedQueryResponse
    : PagedResponse<RideResponse>;