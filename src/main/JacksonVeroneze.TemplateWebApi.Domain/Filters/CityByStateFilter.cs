using JacksonVeroneze.NET.Pagination;

namespace JacksonVeroneze.TemplateWebApi.Domain.Filters;

public record CityByStateFilter
{
    public string? StateId { get; set; }

    public PaginationParameters? Pagination { get; set; }
}