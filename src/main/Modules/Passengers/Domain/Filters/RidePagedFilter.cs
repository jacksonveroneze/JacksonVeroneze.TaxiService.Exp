using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.NET.Pagination.Cursor;
using TaxiService.Modules.Passengers.Domain.Enums;

namespace TaxiService.Modules.Passengers.Domain.Filters;

[ExcludeFromCodeCoverage]
public record RidePagedFilter
{
    public RideStatus? Status { get; init; }

    public Guid? UserId { get; init; }

    public PaginationParameters? Pagination { get; init; }
}