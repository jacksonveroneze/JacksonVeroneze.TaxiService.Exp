using JacksonVeroneze.NET.EntityFramework.Interfaces;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.User;
using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Infrastructure.EfCore.Repositories.User;

[ExcludeFromCodeCoverage]
public class UserWriteRepository(
    IEfCoreRepository<UserEntity> service) : IUserWriteRepository
{
    public async Task CreateAsync(UserEntity entity,
        CancellationToken cancellationToken = default)
    {
        await service.CreateAsync(entity, cancellationToken);
    }

    public Task DeleteAsync(UserEntity entity,
        CancellationToken cancellationToken = default)
    {
        service.Delete(entity);

        return service.UnitOfWork.CommitAsync(cancellationToken);
    }

    public Task UpdateAsync(UserEntity entity,
        CancellationToken cancellationToken = default)
    {
        service.Update(entity);

        return service.UnitOfWork.CommitAsync(cancellationToken);
    }
}