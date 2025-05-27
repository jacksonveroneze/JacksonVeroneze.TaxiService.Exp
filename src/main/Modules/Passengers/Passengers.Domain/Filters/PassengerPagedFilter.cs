using System.Diagnostics.CodeAnalysis;

using JacksonVeroneze.NET.Pagination.Cursor;

using TaxiService.Modules.Passengers.Domain.Enums;

namespace TaxiService.Modules.Passengers.Domain.Filters;

[ExcludeFromCodeCoverage]
public record PassengerPagedFilter : PassengerFilter
{
    public Guid? PassengerId { get; init; }
    
    public PassengerStatus? Status { get; init; }

    public PaginationParameters? Pagination { get; init; }
}
