using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.NET.Pagination.Offset;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Filters;

[ExcludeFromCodeCoverage]
public record EmailPagedFilter
{
    public Guid? UserId { get; init; }

    public PaginationParameters? Pagination { get; init; }
}