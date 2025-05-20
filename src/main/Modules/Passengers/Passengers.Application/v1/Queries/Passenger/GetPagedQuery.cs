using JacksonVeroneze.NET.Pagination.Enums;
using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Queries.Base;
using TaxiService.Modules.Passengers.Application.v1.Models.Passenger;
using TaxiService.Modules.Passengers.Domain.Enums;

namespace TaxiService.Modules.Passengers.Application.v1.Queries.Passenger;

public sealed record GetPagedQuery() :
    PagedCursorQuery(DefaultOrderBy, DefaultOrder),
    IRequest<Result<GetPagedQueryResponse>>
{
    private const string DefaultOrderBy = nameof(PassengerResponse.FullName);

    private const SortDirection DefaultOrder = SortDirection.Ascending;

    public string? Name { get; init; }

    public PassengerStatus? Status { get; init; }

    public DateOnly? StartBirthday { get; init; }

    public DateOnly? EndBirthday { get; init; }
}
