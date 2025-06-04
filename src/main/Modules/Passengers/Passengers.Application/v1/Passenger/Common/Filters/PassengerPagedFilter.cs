using System.Diagnostics.CodeAnalysis;

using JacksonVeroneze.NET.Pagination.Cursor;

using TaxiService.Modules.Passengers.Domain.Enums;

namespace TaxiService.Modules.Passengers.Application.v1.Passenger.Common.Filters;

[ExcludeFromCodeCoverage]
public record PassengerPagedFilter : PassengerFilter
{
    public PassengerStatus? Status { get; init; }

    public PaginationParameters? Pagination { get; init; }
}
