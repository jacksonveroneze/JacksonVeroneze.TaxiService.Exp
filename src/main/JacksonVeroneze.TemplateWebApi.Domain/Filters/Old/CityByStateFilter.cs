using JacksonVeroneze.NET.Pagination;

namespace JacksonVeroneze.TemplateWebApi.Domain.Filters.Old;

public record CityByStateFilter
{
    public string? StateId { get; init; }

    public PaginationParameters? Pagination { get; init; }
}
