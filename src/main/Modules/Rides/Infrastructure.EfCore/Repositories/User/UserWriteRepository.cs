using JacksonVeroneze.NET.EntityFramework.Interfaces;
using TaxiService.Modules.Rides.Application.v1.Interfaces.Repositories.User;
using TaxiService.Modules.Rides.Domain.Entities;

namespace TaxiService.Modules.Rides.Infrastructure.EfCore.Repositories.User;

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