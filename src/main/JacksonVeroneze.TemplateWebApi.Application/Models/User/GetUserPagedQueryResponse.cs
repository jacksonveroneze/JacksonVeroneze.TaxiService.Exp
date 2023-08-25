using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response.Pagination;

namespace JacksonVeroneze.TemplateWebApi.Application.Models.User;

public sealed record GetUserPagedQueryResponse : PagedResponse<UserResponse>
{
}
