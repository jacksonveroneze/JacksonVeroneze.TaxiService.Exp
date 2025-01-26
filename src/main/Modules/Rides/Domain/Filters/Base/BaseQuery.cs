using JacksonVeroneze.NET.Pagination.Cursor;

namespace TaxiService.Modules.Rides.Domain.Filters.Base;

public record BaseQuery<T>
{
    public T? Filter { get; set; }

    public PaginationParameters? Pagination { get; set; }
}
