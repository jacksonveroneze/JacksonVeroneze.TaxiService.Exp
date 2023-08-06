using JacksonVeroneze.NET.Pagination;

namespace JacksonVeroneze.TemplateWebApi.Domain.Filters.Old;

public record StateAllFilter
{
    public PaginationParameters? Pagination { get; init; }
}
