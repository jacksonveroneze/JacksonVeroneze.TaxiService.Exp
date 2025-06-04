using JacksonVeroneze.NET.Pagination.Enums;
using JacksonVeroneze.NET.Result;

using TaxiService.Common.BaseApplication.Queries.Base;
using TaxiService.Modules.Rides.Application.v1.Ride.Common.Enums;
using TaxiService.Modules.Rides.Application.v1.Ride.Common.Models;
using TaxiService.Modules.Rides.Domain.Enums;

namespace TaxiService.Modules.Rides.Application.v1.Ride.GetPaged;

public sealed record GetRidePagedQuery() :
    PagedCursorQuery(DefaultOrderBy, DefaultOrder),
    IRequest<Result<GetRidePagedQueryResponse>>
{
    private const string DefaultOrderBy = nameof(RideResponse.Id);

    private const SortDirection DefaultOrder = SortDirection.Ascending;

    public RideStatus? Status { get; init; }

    public Guid? PassengerId { get; init; }

    public RideExpands[]? Expand { get; init; }
}
