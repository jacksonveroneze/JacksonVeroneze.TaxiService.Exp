using JacksonVeroneze.NET.Pagination.Cursor;

namespace TaxiService.Common.BaseInfrastructure;

public static class CursorPaginator
{
    public static Page<T> Paginate<T>(
        ICollection<T> items,
        int pageSize,
        Func<T, string?> getCursor) where T : class
    {
        ArgumentNullException.ThrowIfNull(items);
        ArgumentNullException.ThrowIfNull(getCursor);

        T[] data = items.Take(pageSize).ToArray();

        bool hasMore = items.Count > pageSize;

        string? cursor = hasMore
            ? getCursor(data[^1])
            : null;

        return new Page<T>(data,
            new PageInfo(hasMore, cursor));
    }
}
