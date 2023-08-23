using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.Client;

public interface IClientWriteRepository
{
    Task CreateAsync(
        ClientEntity entity,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        ClientEntity entity,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        ClientEntity entity,
        CancellationToken cancellationToken = default);
}
