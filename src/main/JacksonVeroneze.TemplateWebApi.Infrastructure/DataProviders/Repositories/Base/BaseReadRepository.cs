using System.Linq.Expressions;
using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.NET.Pagination.Extensions;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.Extensions;
using JacksonVeroneze.TemplateWebApi.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.Base;

[ExcludeFromCodeCoverage]
public class BaseReadRepository<TEntity> where TEntity : class
{
    private readonly List<TEntity> _empty =
        Enumerable.Empty<TEntity>().ToList();

    private readonly ILogger<BaseReadRepository<TEntity>> _logger;
    private readonly DbSet<TEntity> _dbSet;

    public BaseReadRepository(ILogger<BaseReadRepository<TEntity>> logger,
        DbContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        _logger = logger;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<TEntity?> GetByIdAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        TEntity? result = await _dbSet.FindAsync(
            new object[] { id }, cancellationToken);

        if (result is null)
        {
            _logger.LogNotFound(nameof(BaseReadRepository<TEntity>),
                nameof(GetByIdAsync), id, DomainErrors.Ride.NotFound);
        }

        return result;
    }

    public async Task<Page<TEntity>> GetPagedAsync(
        Expression<Func<TEntity, bool>> filter,
        PaginationParameters paginationParameters,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(filter);

        int count = await _dbSet
            .AsNoTracking()
            .Where(filter)
            .CountAsync(cancellationToken);

        List<TEntity> result = count == 0
            ? _empty
            : await _dbSet
                .AsNoTracking()
                .Where(filter)
                .ConfigurePagination(paginationParameters)
                .ToListAsync(cancellationToken);

        Page<TEntity> data = result
            .ToPage(paginationParameters, count);

        _logger.LogGetPaged(nameof(BaseReadRepository<TEntity>),
            nameof(GetPagedAsync),
            data.Pagination.Page,
            data.Pagination.PageSize,
            data.Pagination.TotalElements);

        return data;
    }
}
