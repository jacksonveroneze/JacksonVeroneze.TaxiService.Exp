using JacksonVeroneze.NET.EntityFramework.Interfaces;

using TaxiService.Modules.Passengers.Application.Common.Interfaces.Repositories.Email;
using TaxiService.Modules.Passengers.Domain.Entities;
using TaxiService.Modules.Passengers.Infrastructure.Contexts;

namespace TaxiService.Modules.Passengers.Infrastructure.Repositories.Email;

[ExcludeFromCodeCoverage]
public class EmailWriteRepository(
    IEfCoreRepository<EmailEntity, PassengersDbContext> service)
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