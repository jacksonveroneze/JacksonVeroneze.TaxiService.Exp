using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TaxiService.Exp.Domain.Enums;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Filters;

[ExcludeFromCodeCoverage]
public record UserPagedFilter
{
    public string? Name { get; init; }

    public UserStatus? Status { get; init; }

    public PaginationParameters? Pagination { get; init; }
}