using JacksonVeroneze.NET.EntityFramework.Interfaces;
using Microsoft.Extensions.Logging;
using TaxiService.Common.BaseInfrastructure.Extensions;
using TaxiService.Modules.Passenger.Domain;
using TaxiService.Modules.Passenger.Domain.Repositories;
using TaxiService.Modules.Passenger.Infrastructure.Contexts;

namespace TaxiService.Modules.Passenger.Infrastructure.Repositories.Passenger;

[ExcludeFromCodeCoverage]
public class PassengerRepository(
    ILogger<PassengerRepository> logger,
    IEfCoreRepository<Domain.Aggregates.Passenger, PassengersDbContext> service)
    : IPassengerRepository
{
    public async Task<Domain.Aggregates.Passenger?> GetByIdAsync(
        Guid id, CancellationToken cancellationToken)
    {
        Domain.Aggregates.Passenger? result = await service.GetByIdAsync(
            conf => conf.Id == id,
            cancellationToken);

        logger.LogGetById(
            ModuleConstants.ContextName,
            nameof(PassengerRepository),
            nameof(GetByIdAsync),
            id, result is not null);

        return result;
    }

    public async Task CreateAsync(Domain.Aggregates.Passenger entity,
        CancellationToken cancellationToken)
    {
        await service.CreateAsync(entity, cancellationToken);
    }

    public async Task DeleteAsync(Domain.Aggregates.Passenger entity,
        CancellationToken cancellationToken)
    {
        service.Delete(entity);

        await service.DbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Domain.Aggregates.Passenger entity,
        CancellationToken cancellationToken)
    {
        service.Update(entity);

        await service.DbContext.SaveChangesAsync(cancellationToken);
    }
}
