using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Base.Pagination;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Ride;

public sealed record GetRidePagedQueryResponse : PagedResponse<RideResponse>
{
}
