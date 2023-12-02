using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.Base;
using JacksonVeroneze.TaxiService.Exp.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.User;

public sealed record GetUserPagedQuery :
    PagedQuery, IRequest<Result<GetUserPagedQueryResponse>>
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
