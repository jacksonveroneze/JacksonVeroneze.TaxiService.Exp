using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response.Pagination;

namespace JacksonVeroneze.TemplateWebApi.Application.Models.Account;

public sealed record GetAccountPagedQueryResponse : PagedResponse<AccountResponse>
{
}
