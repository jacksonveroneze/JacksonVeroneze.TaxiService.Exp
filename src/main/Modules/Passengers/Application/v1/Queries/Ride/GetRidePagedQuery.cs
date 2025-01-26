using JacksonVeroneze.NET.Pagination.Common;
using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Passengers.Application.Queries.Base;
using TaxiService.Modules.Passengers.Application.v1.Models.Ride;
using TaxiService.Modules.Passengers.Domain.Enums;

namespace TaxiService.Modules.Passengers.Application.v1.Queries.Ride;

public sealed record GetRidePagedQuery() :
    PagedCursorQuery(DefaultOrderBy, DefaultOrder),
    IRequest<Result<GetRidePagedQueryResponse>>
{
    private const string DefaultOrderBy = nameof(RideResponse.Id);

    private const SortDirection DefaultOrder = SortDirection.Ascending;

    public RideStatus? Status { get; init; }

    public Guid? UserId { get; init; }
}