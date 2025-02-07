using JacksonVeroneze.NET.Pagination.Common;
using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Queries.Base;
using TaxiService.Modules.Passengers.Application.v1.Models.User;
using TaxiService.Modules.Passengers.Domain.Enums;

namespace TaxiService.Modules.Passengers.Application.v1.Queries.User;

public sealed record GetPagedQuery() :
    PagedCursorQuery(DefaultOrderBy, DefaultOrder),
    IRequest<Result<GetPagedQueryResponse>>
{
    private const string DefaultOrderBy = nameof(UserResponse.FullName);

    private const SortDirection DefaultOrder = SortDirection.Ascending;

    public string? Name { get; init; }

    public UserStatus? Status { get; init; }

    public DateOnly? StartBirthday { get; init; }

    public DateOnly? EndBirthday { get; init; }
}