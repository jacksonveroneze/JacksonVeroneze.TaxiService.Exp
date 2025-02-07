using JacksonVeroneze.NET.EntityFramework.Interfaces;
using TaxiService.Modules.Drivers.Application.v1.Interfaces.Repositories.Driver;
using TaxiService.Modules.Drivers.Domain.Entities;
using TaxiService.Modules.Drivers.Infrastructure.EfCore.Contexts;

namespace TaxiService.Modules.Drivers.Infrastructure.EfCore.Repositories.User;

[ExcludeFromCodeCoverage]
public class DriverWriteRepository(
    IEfCoreRepository<DriverEntity, DriversDbContext> service) : IDriverWriteRepository
{
    public async Task CreateAsync(DriverEntity entity,
        CancellationToken cancellationToken)
    {
        await service.CreateAsync(entity, cancellationToken);
    }

    public async Task UpdateAsync(DriverEntity entity,
        CancellationToken cancellationToken)
    {
        service.Update(entity);

        await service.DbContext.SaveChangesAsync(cancellationToken);
    }
}