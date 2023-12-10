using System.Linq.Expressions;
using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Ride;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Filters;
using JacksonVeroneze.TaxiService.Exp.Domain.Specifications.Base.Predicate;
using JacksonVeroneze.TaxiService.Exp.Domain.Specifications.Ride;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.Contexts;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.DataProviders.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.DataProviders.Repositories.Ride;

[ExcludeFromCodeCoverage]
public class RideReadRepository :
    BaseReadRepository<RideEntity>, IRideReadRepository
{
    private readonly DbSet<RideEntity> _dbSet;

    public RideReadRepository(ILogger<RideReadRepository> logger,
        ApplicationDbContext context) : base(logger, context)
    {
        ArgumentNullException.ThrowIfNull(context);

        _dbSet = context.Set<RideEntity>();
    }

    public async Task<bool> ExistsActiveByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        RideUserIdSpecification specUserId = new(userId);

        RideStatusSpecification specStatusRequested =
            new();

        Expression<Func<RideEntity, bool>> spec =
            specUserId.ToExpression()
                .And(specStatusRequested.ToExpression());

        bool exists = await _dbSet.AnyAsync(spec,
            cancellationToken);

        return exists;
    }

    public Task<Page<RideEntity>> GetPagedAsync(
        RidePagedFilter filter,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(filter);

        RideStatusFilterSpecification specStatus = new(filter.Status);
        RideUserIdSpecification specUserId = new(filter.UserId);

        Expression<Func<RideEntity, bool>> spec =
            specStatus.ToExpression().And(specUserId);

        return GetPagedAsync(spec, filter.Pagination!,
            cancellationToken);
    }
}
