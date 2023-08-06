using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;

namespace JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;

public interface IClientRepository
{
    Task<Client?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<Page<Client>> GetPagedAsync(
        ClientPagedFilter filter,
        CancellationToken cancellationToken = default);

    Task CreateAsync(
        Client client,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        Client client,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        Client client,
        CancellationToken cancellationToken = default);
}
