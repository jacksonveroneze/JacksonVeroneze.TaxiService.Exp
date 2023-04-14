using JacksonVeroneze.NET.Pagination;

namespace JacksonVeroneze.TemplateWebApi.Domain.Filters;

public record StateAllFilter
{
    public PaginationParameters? Pagination { get; set; }
}