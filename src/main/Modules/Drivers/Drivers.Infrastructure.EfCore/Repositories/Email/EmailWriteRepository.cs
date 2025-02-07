using JacksonVeroneze.NET.EntityFramework.Interfaces;
using TaxiService.Modules.Drivers.Application.v1.Interfaces.Repositories.Email;
using TaxiService.Modules.Drivers.Domain.Entities;
using TaxiService.Modules.Drivers.Infrastructure.EfCore.Contexts;

namespace TaxiService.Modules.Drivers.Infrastructure.EfCore.Repositories.Email;

[ExcludeFromCodeCoverage]
public class EmailWriteRepository(
    IEfCoreRepository<EmailEntity, DriversDbContext> service)
    : IEmailWriteRepository
{
    public async Task CreateAsync(EmailEntity entity,
        CancellationToken cancellationToken)
    {
        await service.CreateAsync(
            entity, cancellationToken);

        await service.DbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(EmailEntity entity,
        CancellationToken cancellationToken)
    {
        service.Delete(entity);

        await service.DbContext.SaveChangesAsync(cancellationToken);
    }
}