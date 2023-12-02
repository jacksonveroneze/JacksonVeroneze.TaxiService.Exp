using JacksonVeroneze.TaxiService.Exp.Domain.Entities;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.User;

public interface IUserWriteRepository
{
    Task CreateAsync(
        UserEntity entity,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        UserEntity entity,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        UserEntity entity,
        CancellationToken cancellationToken = default);
}
