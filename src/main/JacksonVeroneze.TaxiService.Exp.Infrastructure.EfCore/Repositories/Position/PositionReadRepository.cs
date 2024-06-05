using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Position;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.Contexts;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.Repositories.Position;

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

    public async Task<ICollection<PositionEntity>> GetByRideIdAsync(
        Guid rideId,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .AsNoTrackingWithIdentityResolution()
            .Where(conf => conf.RideId == rideId)
            .ToListAsync(cancellationToken);
    }
}