using JacksonVeroneze.NET.EntityFramework.Interfaces;
using Microsoft.Extensions.Logging;
using TaxiService.Common.BaseInfrastructure.Extensions;
using TaxiService.Modules.Driver.Application.Common;
using TaxiService.Modules.Driver.Domain.Repositories;
using TaxiService.Modules.Driver.Infrastructure.Contexts;

namespace TaxiService.Modules.Driver.Infrastructure.Repositories.Vehicle;

[ExcludeFromCodeCoverage]
public class VehicleRepository(
    ILogger<VehicleRepository> logger,
    IEfCoreRepository<Domain.Aggregates.Vehicle, DriversDbContext> service)
    : IVehicleRepository
{
    public async Task<Domain.Aggregates.Vehicle?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        Domain.Aggregates.Vehicle? result = await service.GetByIdAsync(
            conf => conf.Id == id,
            cancellationToken);

        logger.LogGetById(ModuleContext.ContextName,
            nameof(VehicleRepository),
            nameof(GetByIdAsync),
            id, result is not null);

        return result;
    }

    public async Task CreateAsync(Domain.Aggregates.Vehicle entity,
        CancellationToken cancellationToken)
    {
        await service.CreateAsync(entity, cancellationToken);
    }

    public async Task UpdateAsync(Domain.Aggregates.Vehicle entity,
        CancellationToken cancellationToken)
    {
        service.Update(entity);

        await service.DbContext.SaveChangesAsync(cancellationToken);
    }
}
