using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Position;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Specifications.Position;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.Contexts;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.DataProviders.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.DataProviders.Repositories.Position;

[ExcludeFromCodeCoverage]
public class PositionReadRepository :
    BaseReadRepository<PositionEntity>, IPositionReadRepository
{
    private readonly DbSet<PositionEntity> _dbSet;

    public PositionReadRepository(
        ILogger<PositionReadRepository> logger,
        ApplicationDbContext context) : base(logger, context)
    {
        ArgumentNullException.ThrowIfNull(context);

        _dbSet = context.Set<PositionEntity>();
    }

    public Task<List<PositionEntity>> GetByRideIdAsync(
        Guid rideId,
        CancellationToken cancellationToken = default)
    {
        PositionRideIdSpecification spec = new(rideId);

        return _dbSet
            .AsNoTrackingWithIdentityResolution()
            .Where(spec.ToExpression())
            .ToListAsync(cancellationToken);
    }
}