using JacksonVeroneze.NET.EntityFramework.Interfaces;

using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Passenger;
using TaxiService.Modules.Passengers.Domain.Entities;
using TaxiService.Modules.Passengers.Infrastructure.Contexts;

namespace TaxiService.Modules.Passengers.Infrastructure.Repositories.Passenger;

[ExcludeFromCodeCoverage]
public class PassengerWriteRepository(
    IEfCoreRepository<PassengerEntity, PassengersDbContext> service) : IPassengerWriteRepository
{
    public async Task CreateAsync(PassengerEntity entity,
        CancellationToken cancellationToken)
    {
        await service.CreateAsync(entity, cancellationToken);
    }

    public async Task DeleteAsync(PassengerEntity entity,
        CancellationToken cancellationToken)
    {
        service.Delete(entity);

        await service.DbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(PassengerEntity entity,
        CancellationToken cancellationToken)
    {
        service.Update(entity);

        await service.DbContext.SaveChangesAsync(cancellationToken);
    }
}
