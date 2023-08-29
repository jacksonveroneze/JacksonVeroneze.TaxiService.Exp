using JacksonVeroneze.NET.DomainObjects.Result;
using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Application.Models.User;
using JacksonVeroneze.TemplateWebApi.Application.Queries.Base;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Application.Queries.User;

public sealed record GetUserPagedQuery :
    PagedQuery, IRequest<IResult<GetUserPagedQueryResponse>>
{
    private const string DefaultOrderBy = nameof(UserResponse.Name);

    private const SortDirection DefaultOrder = SortDirection.Ascending;

    [FromQuery(Name = "name")]
    public string? Name { get; init; }

    [FromQuery(Name = "status")]
    public UserStatus? Status { get; init; }

    public GetUserPagedQuery() : base(DefaultOrderBy, DefaultOrder)
    {
    }
}
