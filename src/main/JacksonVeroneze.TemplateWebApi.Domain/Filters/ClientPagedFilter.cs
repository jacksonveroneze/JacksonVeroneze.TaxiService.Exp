namespace JacksonVeroneze.TemplateWebApi.Domain.Filters;

public record ClientPagedFilter
{
    public string? Name { get; init; }

    public string? Mail { get; init; }
}
