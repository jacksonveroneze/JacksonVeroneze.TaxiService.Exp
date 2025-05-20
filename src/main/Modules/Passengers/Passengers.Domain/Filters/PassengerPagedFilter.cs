using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.NET.Pagination.Cursor;

namespace TaxiService.Modules.Passengers.Domain.Filters;

[ExcludeFromCodeCoverage]
public record PassengerPagedFilter
{
    public Guid? PassengerId { get; init; }

    public PaginationParameters? Pagination { get; init; }
}
