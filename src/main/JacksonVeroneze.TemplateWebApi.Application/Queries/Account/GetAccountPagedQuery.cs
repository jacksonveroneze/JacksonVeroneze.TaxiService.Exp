using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Application.Models.Account;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;
using JacksonVeroneze.TemplateWebApi.Application.Queries.Base;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Primitives;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Application.Queries.Account;

public class GetAccountPagedQuery : PagedQuery, IRequest<IResult<BaseResponse>>
{
    private const string DefaultOrderBy = nameof(AccountResponse.Id);

    private const SortDirection DefaultOrder = SortDirection.Ascending;

    [FromRoute(Name = "client_id")]
    public Guid ClientId { get; init; }

    public GetAccountPagedQuery() : base(DefaultOrderBy, DefaultOrder)
    {
    }
}
