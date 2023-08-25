using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.User;

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
