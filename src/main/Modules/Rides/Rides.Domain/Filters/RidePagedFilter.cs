using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.NET.Pagination.Cursor;
using TaxiService.Modules.Rides.Domain.Enums;

namespace TaxiService.Modules.Rides.Domain.Filters;

[ExcludeFromCodeCoverage]
public record RidePagedFilter
{
    public RideStatus? Status { get; init; }

    public Guid? PassangerId { get; init; }

    public PaginationParameters? Pagination { get; init; }
}