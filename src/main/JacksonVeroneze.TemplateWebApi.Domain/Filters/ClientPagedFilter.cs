using JacksonVeroneze.NET.Pagination;

namespace JacksonVeroneze.TemplateWebApi.Domain.Filters;

public record ClientPagedFilter
{
    public string? Name { get; init; }

    public string? Email { get; init; }

    public PaginationParameters? Pagination { get; init; }
}
