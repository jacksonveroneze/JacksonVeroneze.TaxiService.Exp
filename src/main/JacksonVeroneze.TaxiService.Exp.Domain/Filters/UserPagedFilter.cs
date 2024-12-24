using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.NET.Pagination.Cursor;
using JacksonVeroneze.TaxiService.Exp.Domain.Enums;
using JacksonVeroneze.TaxiService.Exp.Domain.Util;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Filters;

[ExcludeFromCodeCoverage]
public record UserPagedFilter
{
    public StringFilter? NameFilter { get; set; }

    public DateOnlyFilter? StartBirthdayFilter { get; set; }

    public DateOnlyFilter? EndBirthdayFilter { get; set; }

    public EnumFilter<UserStatus>? StatusFilter { get; set; }

    public PaginationParameters? Pagination { get; init; }
}