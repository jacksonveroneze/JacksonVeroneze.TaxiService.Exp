using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.NET.Pagination.Cursor;

namespace TaxiService.Modules.Driver.Application.v1.Driver.Common.Filters;

[ExcludeFromCodeCoverage]
public record DriverPagedFilter : DriverFilter
{
    public string? Cursor { get; init; }

    public string? Name { get; init; }

    public PaginationParameters? Pagination { get; init; }
}
