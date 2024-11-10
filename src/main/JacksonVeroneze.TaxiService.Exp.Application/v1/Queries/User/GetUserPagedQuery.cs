using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.Base;
using JacksonVeroneze.TaxiService.Exp.Domain.Enums;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.User;

public sealed record GetUserPagedQuery() :
    PagedQuery(DefaultOrderBy, DefaultOrder),
    IRequest<Result<GetUserPagedQueryResponse>>
{
    private const string DefaultOrderBy = nameof(UserResponse.FullName);

    private const SortDirection DefaultOrder = SortDirection.Ascending;

    public string? Name { get; init; }

    public UserStatus? Status { get; init; }

    public string? Cursor { get; init; }
}