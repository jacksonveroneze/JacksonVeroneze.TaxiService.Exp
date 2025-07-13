using JacksonVeroneze.NET.EntityFramework.Interfaces;
using Microsoft.Extensions.Logging;
using Ride.Common;
using TaxiService.Common.BaseInfrastructure.Extensions;
using TaxiService.Modules.Ride.Domain.Repositories;
using TaxiService.Modules.Ride.Infrastructure.Contexts;

namespace TaxiService.Modules.Ride.Infrastructure.Repositories.Ride;

[ExcludeFromCodeCoverage]
public class RideRepository(
    ILogger<RideRepository> logger,
    IEfCoreRepository<Domain.Aggregates.Ride, RidesDbContext> service)
    : IRideRepository
{
    public async Task<Domain.Aggregates.Ride?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        Domain.Aggregates.Ride? result = await service.GetByIdAsync(
            conf => conf.Id == id,
            cancellationToken);

        logger.LogGetById(
            ModuleConstants.ContextName,
            nameof(RideRepository),
            nameof(GetByIdAsync),
            id, result is not null);

        return result;
    }

    public async Task CreateAsync(Domain.Aggregates.Ride entity,
        CancellationToken cancellationToken)
    {
        await service.CreateAsync(entity, cancellationToken);

        await service.DbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Domain.Aggregates.Ride entity,
        CancellationToken cancellationToken)
    {
        service.Update(entity);

        await service.DbContext.SaveChangesAsync(cancellationToken);
    }
}
