using JacksonVeroneze.NET.Pagination.Enums;
using JacksonVeroneze.NET.Result;

using TaxiService.Common.BaseApplication.Queries.Base;
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

    public string? Expands { get; init; }

    public HashSet<RideExpands>? Expand
    {
        get
        {
            IEnumerable<RideExpands> expands = Expands?.Split(',', 
                StringSplitOptions.RemoveEmptyEntries)
                .Select(Enum.Parse<RideExpands>) ?? [];

            return [..expands];
        }
    }
}
