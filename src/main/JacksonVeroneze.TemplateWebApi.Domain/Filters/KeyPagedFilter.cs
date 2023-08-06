using JacksonVeroneze.TemplateWebApi.Domain.Enums;

namespace JacksonVeroneze.TemplateWebApi.Domain.Filters;

public record KeyPagedFilter
{
    public KeyType? Type { get; init; }

    public KeyStatus? Status { get; init; }
}
