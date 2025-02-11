using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.NET.Pagination.Cursor;

namespace TaxiService.Modules.Passengers.Domain.Filters;

[ExcludeFromCodeCoverage]
public record PassangerPagedFilter
{
    public Guid? PassangerId { get; init; }

    public PaginationParameters? Pagination { get; init; }
}