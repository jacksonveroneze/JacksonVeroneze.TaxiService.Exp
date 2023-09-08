using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Base.Pagination;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Models.User;

public sealed record GetUserPagedQueryResponse : PagedResponse<UserResponse>
{
}
