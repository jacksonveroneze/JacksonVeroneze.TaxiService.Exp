using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Base.Pagination;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Models.Ride;

public sealed record GetRidePagedQueryResponse : PagedResponse<RideResponse>
{
}
