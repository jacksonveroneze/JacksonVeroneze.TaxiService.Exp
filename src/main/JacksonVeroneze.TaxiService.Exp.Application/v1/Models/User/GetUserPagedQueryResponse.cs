using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Base.Pagination;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Models.User;

public sealed record GetUserPagedQueryResponse : PagedResponse<UserResponse>
{
}
