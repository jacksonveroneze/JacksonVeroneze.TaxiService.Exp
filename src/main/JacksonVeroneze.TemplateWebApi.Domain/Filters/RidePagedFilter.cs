using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;

namespace JacksonVeroneze.TemplateWebApi.Domain.Filters;

[ExcludeFromCodeCoverage]
public record RidePagedFilter
{
    public RideStatus? Status { get; init; }

    public Guid? UserId { get; init; }

    public PaginationParameters? Pagination { get; init; }
}
