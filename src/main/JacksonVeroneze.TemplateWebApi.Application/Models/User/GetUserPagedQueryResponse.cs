using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Pagination;

namespace JacksonVeroneze.TemplateWebApi.Application.Models.User;

public sealed record GetUserPagedQueryResponse : PagedResponse<UserResponse>
{
}
