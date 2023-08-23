using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.Client;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.Client.Stub;

public class ClientWriteStubRepository : IClientWriteRepository
{
    public Task CreateAsync(ClientEntity entity,
        CancellationToken cancellationToken = default)
    {
        ClientDatabase.Data.Add(entity);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(ClientEntity entity,
        CancellationToken cancellationToken = default)
    {
        ClientDatabase.Data.Remove(entity);

        return Task.CompletedTask;
    }

    public Task UpdateAsync(ClientEntity entity,
        CancellationToken cancellationToken = default)
    {
        ClientDatabase.Data.Remove(entity);

        ClientDatabase.Data.Add(entity);

        return Task.CompletedTask;
    }
}
