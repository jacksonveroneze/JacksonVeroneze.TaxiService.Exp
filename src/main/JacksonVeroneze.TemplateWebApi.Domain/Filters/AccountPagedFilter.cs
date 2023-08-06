using JacksonVeroneze.TemplateWebApi.Domain.Enums;

namespace JacksonVeroneze.TemplateWebApi.Domain.Filters;

public record AccountPagedFilter
{
    public string? Number { get; init; }

    public AccountStatus? Status { get; init; }
}
