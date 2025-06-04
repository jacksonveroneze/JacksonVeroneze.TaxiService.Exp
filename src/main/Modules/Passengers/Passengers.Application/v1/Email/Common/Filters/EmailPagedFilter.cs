using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.NET.Pagination.Offset;

namespace TaxiService.Modules.Passengers.Application.v1.Email.Common.Filters;

[ExcludeFromCodeCoverage]
public record EmailPagedFilter
{
    public Guid? PassengerId { get; init; }

    public PaginationParameters? Pagination { get; init; }
}
