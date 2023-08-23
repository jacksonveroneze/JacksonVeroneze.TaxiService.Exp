using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Application.Models.Client;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;
using JacksonVeroneze.TemplateWebApi.Application.Primitives;
using JacksonVeroneze.TemplateWebApi.Application.Queries.Base;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Application.Queries.Client;

public class GetClientPagedQuery : PagedQuery, IRequest<IResult<BaseResponse>>
{
    private const string DefaultOrderBy = nameof(ClientResponse.Id);

    private const SortDirection DefaultOrder = SortDirection.Ascending;

    [FromQuery(Name = "name")]
    public string? Name { get; init; }

    [FromQuery(Name = "email")]
    public string? Email { get; init; }

    public GetClientPagedQuery() : base(DefaultOrderBy, DefaultOrder)
    {
    }
}
