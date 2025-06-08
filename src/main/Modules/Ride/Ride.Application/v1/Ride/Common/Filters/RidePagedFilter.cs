using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.NET.Pagination.Cursor;
using TaxiService.Modules.Ride.Domain.Enums;

namespace TaxiService.Modules.Ride.Application.v1.Ride.Common.Filters;

[ExcludeFromCodeCoverage]
public record RidePagedFilter
{
    public RideStatus? Status { get; init; }

    public Guid? PassengerId { get; init; }

    public PaginationParameters? Pagination { get; init; }
}
