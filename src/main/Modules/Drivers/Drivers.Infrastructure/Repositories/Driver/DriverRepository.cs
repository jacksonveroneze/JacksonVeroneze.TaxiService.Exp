using JacksonVeroneze.NET.EntityFramework.Interfaces;

using TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories.Driver;
using TaxiService.Modules.Drivers.Domain.Entities;
using TaxiService.Modules.Drivers.Infrastructure.Contexts;

namespace TaxiService.Modules.Drivers.Infrastructure.Repositories.Driver;

[ExcludeFromCodeCoverage]
public class DriverRepository(
    IEfCoreRepository<DriverEntity, DriversDbContext> service)
    : IDriverRepository
{
    public Task<DriverEntity?> GetByIdAsync(
        Guid id, CancellationToken cancellationToken)
    {
        return service.GetByIdAsync(conf => conf.Id == id,
            cancellationToken);
    }

    public async Task CreateAsync(DriverEntity entity,
        CancellationToken cancellationToken)
    {
        await service.CreateAsync(entity, cancellationToken);
    }

    public Task DeleteAsync(DriverEntity entity,
        CancellationToken cancellationToken)
    {
        entity.MarkAsDeleted();

        service.SoftDelete(entity);

        return service.DbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(DriverEntity entity,
        CancellationToken cancellationToken)
    {
        service.Update(entity);

        await service.DbContext.SaveChangesAsync(cancellationToken);
    }
}
