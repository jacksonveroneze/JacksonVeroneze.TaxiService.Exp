using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.User;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.User.Stub;

public class UserWriteStubRepository : IUserWriteRepository
{
    public Task CreateAsync(UserEntity entity,
        CancellationToken cancellationToken = default)
    {
        UserDatabase.Data.Add(entity);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(UserEntity entity,
        CancellationToken cancellationToken = default)
    {
        UserDatabase.Data.Remove(entity);

        return Task.CompletedTask;
    }

    public Task UpdateAsync(UserEntity entity,
        CancellationToken cancellationToken = default)
    {
        UserDatabase.Data.Remove(entity);

        UserDatabase.Data.Add(entity);

        return Task.CompletedTask;
    }
}
