using System.Linq.Expressions;
using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.NET.Pagination.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.Ride;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;
using JacksonVeroneze.TemplateWebApi.Domain.Specifications.Ride;
using JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.User.EntityFramework.Extensions;
using Microsoft.EntityFrameworkCore;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.Ride.EntityFramework;

[ExcludeFromCodeCoverage]
public class RideReadRepository : IRideReadRepository
{
    private readonly List<RideEntity> _empty =
        Enumerable.Empty<RideEntity>().ToList();

    private readonly DbSet<RideEntity> _dbSet;

    public RideReadRepository(DbContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        _dbSet = context.Set<RideEntity>();
    }

    public Task<bool> ExistsAsync(string document,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<RideEntity?> GetByIdAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        RideEntity? result = await _dbSet.FindAsync(
            new object[] { id }, cancellationToken);

        return result;
    }

    public async Task<Page<RideEntity>> GetPagedAsync(
        RidePagedFilter filter,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(filter);

        RideStatusSpecification specStatus = new(filter.Status);

        Expression<Func<RideEntity, bool>> spec =
            specStatus;

        int count = await _dbSet
            .AsNoTracking()
            .Where(spec)
            .CountAsync(cancellationToken);

        List<RideEntity> result = count == 0
            ? _empty
            : await _dbSet
                .AsNoTracking()
                .Where(spec)
                .ConfigurePagination(filter.Pagination!)
                .OrderByDescending(ord => ord.CreatedAt)
                .ToListAsync(cancellationToken);

        Page<RideEntity> data = result
            .ToPage(filter.Pagination!, count);

        return data;
    }
}
