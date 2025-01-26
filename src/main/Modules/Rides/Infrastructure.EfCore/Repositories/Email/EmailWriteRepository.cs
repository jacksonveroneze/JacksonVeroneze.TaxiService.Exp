using JacksonVeroneze.NET.EntityFramework.Interfaces;
using TaxiService.Modules.Rides.Application.v1.Interfaces.Repositories.Email;
using TaxiService.Modules.Rides.Domain.Entities;

namespace TaxiService.Modules.Rides.Infrastructure.EfCore.Repositories.Email;

[ExcludeFromCodeCoverage]
public class EmailWriteRepository(
    IEfCoreRepository<EmailEntity> service)
    : IEmailWriteRepository
{
    public async Task CreateAsync(EmailEntity entity,
        CancellationToken cancellationToken = default)
    {
        await service.CreateAsync(
            entity, cancellationToken);
    }

    public async Task DeleteAsync(EmailEntity entity,
        CancellationToken cancellationToken = default)
    {
        service.Delete(entity);

        await service.UnitOfWork.CommitAsync(
            cancellationToken);
    }
}