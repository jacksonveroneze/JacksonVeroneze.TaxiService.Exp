using JacksonVeroneze.NET.EntityFramework.Interfaces;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Email;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.Repositories.Email;

[ExcludeFromCodeCoverage]
public class EmailWriteRepository(
    IEfCoreRepository<EmailEntity> service)
    : IEmailWriteRepository
{
    public async Task CreateAsync(EmailEntity entity,
        CancellationToken cancellationToken = default)
    {
        await service.CreateAsync(entity, cancellationToken);

        await service.UnitOfWork.CommitAsync(cancellationToken);
    }

    public async Task DeleteAsync(EmailEntity entity,
        CancellationToken cancellationToken = default)
    {
        service.Delete(entity);

        await service.UnitOfWork.CommitAsync(cancellationToken);
    }
}