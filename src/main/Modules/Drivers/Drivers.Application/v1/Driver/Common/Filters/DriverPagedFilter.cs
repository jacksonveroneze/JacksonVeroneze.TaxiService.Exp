using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.NET.Pagination.Cursor;
using TaxiService.Modules.Drivers.Domain.Enums;

namespace TaxiService.Modules.Drivers.Application.v1.Driver.Common.Filters;

[ExcludeFromCodeCoverage]
public record DriverPagedFilter : DriverFilter
{
    public string? Cursor { get; init; }

    public string? Name { get; init; }

    public DriverStatus? Status { get; init; }

    public PaginationParameters? Pagination { get; init; }
}
