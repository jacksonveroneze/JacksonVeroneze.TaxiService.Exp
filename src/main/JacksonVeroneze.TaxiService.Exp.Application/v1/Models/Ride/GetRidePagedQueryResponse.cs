using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Base.Pagination.CursorBased;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Ride;

public sealed record GetRidePagedQueryResponse
    : PagedCursorResponse<RideResponse>;