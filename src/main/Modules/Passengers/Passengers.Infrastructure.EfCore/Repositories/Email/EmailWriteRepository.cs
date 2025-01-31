using JacksonVeroneze.NET.EntityFramework.Interfaces;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Email;
using TaxiService.Modules.Passengers.Domain.Entities;
using TaxiService.Modules.Passengers.Infrastructure.EfCore.Contexts;

namespace TaxiService.Modules.Passengers.Infrastructure.EfCore.Repositories.Email;

[ExcludeFromCodeCoverage]
public class EmailWriteRepository(
    IEfCoreRepository<EmailEntity, PassengersDbContext> service)
    : IEmailWriteRepository
{
    public async Task CreateAsync(EmailEntity entity,
        CancellationToken cancellationToken = default)
    {
        await service.CreateAsync(
            entity, cancellationToken);

        await service.DbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(EmailEntity entity,
        CancellationToken cancellationToken = default)
    {
        service.Delete(entity);

        await service.DbContext.SaveChangesAsync(cancellationToken);
    }
}