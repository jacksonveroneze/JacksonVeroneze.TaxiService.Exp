using System.Diagnostics.CodeAnalysis;

using JacksonVeroneze.NET.Pagination.Cursor;

using TaxiService.Modules.Drivers.Domain.Enums;

namespace TaxiService.Modules.Drivers.Domain.Filters;

[ExcludeFromCodeCoverage]
public record DriverPagedFilter
{
    public string? Cursor { get; init; }

    public string? Name { get; init; }

    public DriverStatus? Status { get; init; }

    public PaginationParameters? Pagination { get; init; }
}
