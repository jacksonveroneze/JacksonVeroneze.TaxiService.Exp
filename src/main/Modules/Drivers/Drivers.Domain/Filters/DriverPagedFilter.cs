using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.NET.Pagination.Cursor;

namespace TaxiService.Modules.Drivers.Domain.Filters;

[ExcludeFromCodeCoverage]
public record DriverPagedFilter
{
    public Guid? DriverId { get; init; }

    public PaginationParameters? Pagination { get; init; }
}