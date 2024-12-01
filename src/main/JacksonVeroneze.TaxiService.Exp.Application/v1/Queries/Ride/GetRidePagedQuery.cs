using JacksonVeroneze.NET.Pagination.Common;
using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Ride;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.Base;
using JacksonVeroneze.TaxiService.Exp.Domain.Enums;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.Ride;

public sealed record GetRidePagedQuery() :
    PagedCursorQuery(DefaultOrderBy, DefaultOrder),
    IRequest<Result<GetRidePagedQueryResponse>>
{
    private const string DefaultOrderBy = nameof(RideResponse.Id);

    private const SortDirection DefaultOrder = SortDirection.Ascending;

    public RideStatus? Status { get; init; }

    public Guid? UserId { get; init; }
}