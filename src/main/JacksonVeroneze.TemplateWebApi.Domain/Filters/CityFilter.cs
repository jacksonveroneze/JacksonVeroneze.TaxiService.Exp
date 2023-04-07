using JacksonVeroneze.NET.Pagination;

namespace JacksonVeroneze.TemplateWebApi.Domain.Filters;

public record CityFilter
{
    public string? StateId { get; set; }
    
    public PaginationParameters? Pagination { get; set; }
}