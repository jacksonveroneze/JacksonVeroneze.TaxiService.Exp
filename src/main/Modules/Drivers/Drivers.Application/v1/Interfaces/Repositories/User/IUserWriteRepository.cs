using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.User;

public interface IUserWriteRepository
{
    public Task CreateAsync(
        UserEntity entity,
        CancellationToken cancellationToken = default);

    public Task DeleteAsync(
        UserEntity entity,
        CancellationToken cancellationToken = default);

    public Task UpdateAsync(
        UserEntity entity,
        CancellationToken cancellationToken = default);
}