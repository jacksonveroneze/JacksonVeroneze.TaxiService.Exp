using System.Linq.Expressions;
using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.Ride;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;
using JacksonVeroneze.TemplateWebApi.Domain.Specifications.Base.Predicate;
using JacksonVeroneze.TemplateWebApi.Domain.Specifications.Ride;
using JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.Ride;

[ExcludeFromCodeCoverage]
public class RideReadRepository : BaseReadRepository<RideEntity>, IRideReadRepository
{
    private readonly ILogger<RideReadRepository> _logger;
    private readonly DbSet<RideEntity> _dbSet;

    public RideReadRepository(ILogger<RideReadRepository> logger,
        DbContext context) : base(logger, context)
    {
        ArgumentNullException.ThrowIfNull(context);

        _logger = logger;
        _dbSet = context.Set<RideEntity>();
    }

    public async Task<bool> ExistsByUserAsync(Guid userId,
        CancellationToken cancellationToken = default)
    {
        RideUserIdSpecification specUserId = new(userId);

        RideStatusSpecification specStatusRequested =
            new(RideStatus.Requested);

        RideStatusSpecification specStatusAccepted =
            new(RideStatus.Accepted);

        RideStatusSpecification specStatusInProgress =
            new(RideStatus.InProgress);

        Expression<Func<RideEntity, bool>> spec =
            specUserId.ToExpression()
                .And(specStatusRequested.ToExpression()
                    .Or(specStatusInProgress)
                    .Or(specStatusAccepted));

        bool exists = await _dbSet.AnyAsync(spec,
            cancellationToken);

        return exists;
    }

    public Task<Page<RideEntity>> GetPagedAsync(
        RidePagedFilter filter,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(filter);

        RideStatusSpecification specStatus = new(filter.Status);

        return GetPagedAsync(specStatus, filter.Pagination!,
            cancellationToken);
    }
}
