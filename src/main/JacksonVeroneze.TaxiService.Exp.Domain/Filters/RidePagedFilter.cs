using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TaxiService.Exp.Domain.Enums;
using JacksonVeroneze.TaxiService.Exp.Domain.Results;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Filters;

[ExcludeFromCodeCoverage]
public record RidePagedFilter
{
    public RideStatus? Status { get; init; }

    public Guid? UserId { get; init; }

    public PaginationParameters? Pagination { get; init; }

    public CursorPaginationParameters? CursorPagination { get; set; }
}