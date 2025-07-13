using JacksonVeroneze.NET.Pagination.Enums;
using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Queries;
using TaxiService.Modules.Passenger.Application.v1.Passenger.Common.Models;
using TaxiService.Modules.Passenger.Domain.Enums;

namespace TaxiService.Modules.Passenger.Application.v1.Passenger.GetPaged;

public sealed record GetPassengerPagedQuery() :
    PagedCursorQuery(DefaultOrderBy, DefaultOrder),
    IRequest<Result<GetPassengerPagedQueryResponse>>
{
    private const string DefaultOrderBy = nameof(PassengerResponse.FullName);

    private const SortDirection DefaultOrder = SortDirection.Ascending;

    public string? Name { get; init; }

    public PassengerStatus? Status { get; init; }

    public DateOnly? StartBirthday { get; init; }

    public DateOnly? EndBirthday { get; init; }
}
