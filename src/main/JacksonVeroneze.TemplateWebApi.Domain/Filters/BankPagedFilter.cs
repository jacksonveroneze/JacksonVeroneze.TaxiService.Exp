using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;

namespace JacksonVeroneze.TemplateWebApi.Domain.Filters;

public record BankPagedFilter
{
    public string? Name { get; init; }

    public BankStatus? Status { get; init; }

    public PaginationParameters? Pagination { get; init; }
}
