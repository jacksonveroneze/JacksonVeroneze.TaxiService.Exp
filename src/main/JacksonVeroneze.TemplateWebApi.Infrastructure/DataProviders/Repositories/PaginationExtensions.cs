using JacksonVeroneze.NET.Pagination;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories;

[ExcludeFromCodeCoverage]
public static class PaginationExtensions
{
    public static IQueryable<TSource> ConfigurePagination<TSource>(
        this IQueryable<TSource> queryable,
        PaginationParameters pagination)
    {
        ArgumentNullException.ThrowIfNull(pagination);

        int skip = pagination.Page switch
        {
            < 0 => 0,
            > 0 => pagination.Page - 1,
            _ => pagination.Page
        };

        int take = pagination.PageSize;

        return queryable
            .Skip(skip * take)
            .Take(take);
    }
}
