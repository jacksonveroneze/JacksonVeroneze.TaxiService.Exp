using System.Diagnostics.CodeAnalysis;

using JacksonVeroneze.NET.Pagination.Cursor;

using TaxiService.Modules.Rides.Domain.Enums;

namespace TaxiService.Modules.Rides.Application.v1.Ride.Common.Filters;

[ExcludeFromCodeCoverage]
public record RidePagedFilter
{
    public RideStatus? Status { get; init; }

    public Guid? PassengerId { get; init; }

    public PaginationParameters? Pagination { get; init; }
}
