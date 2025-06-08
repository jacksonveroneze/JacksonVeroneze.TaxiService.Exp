using JacksonVeroneze.NET.EntityFramework.Interfaces;
using TaxiService.Modules.Passenger.Domain.Repositories;
using TaxiService.Modules.Passenger.Infrastructure.Contexts;

namespace TaxiService.Modules.Passenger.Infrastructure.Repositories.Email;

[ExcludeFromCodeCoverage]
public class EmailRepository(
    IEfCoreRepository<Domain.Entities.Email, PassengersDbContext> service)
    : IEmailRepository
{
    public async Task CreateAsync(Domain.Entities.Email entity,
        CancellationToken cancellationToken)
    {
        await service.CreateAsync(
            entity, cancellationToken);

        await service.DbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Domain.Entities.Email entity,
        CancellationToken cancellationToken)
    {
        service.Delete(entity);

        await service.DbContext.SaveChangesAsync(cancellationToken);
    }
}
