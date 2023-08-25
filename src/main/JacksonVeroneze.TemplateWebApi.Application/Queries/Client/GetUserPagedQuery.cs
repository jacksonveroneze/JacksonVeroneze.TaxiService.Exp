using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;
using JacksonVeroneze.TemplateWebApi.Application.Models.User;
using JacksonVeroneze.TemplateWebApi.Application.Queries.Base;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Primitives;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Application.Queries.Client;

public class GetUserPagedQuery : PagedQuery, IRequest<IResult<BaseResponse>>
{
    private const string DefaultOrderBy = nameof(UserResponse.Id);

    private const SortDirection DefaultOrder = SortDirection.Ascending;

    [FromQuery(Name = "name")]
    public string? Name { get; init; }

    [FromQuery(Name = "status")]
    public UserStatus? Status { get; init; }

    public GetUserPagedQuery() : base(DefaultOrderBy, DefaultOrder)
    {
    }
}
