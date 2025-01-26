using JacksonVeroneze.NET.Pagination.Cursor;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Filters.Base;

public record BaseQuery<T>
{
    public T? Filter { get; set; }

    public PaginationParameters? Pagination { get; set; }
}
