using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;

namespace JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;

public interface IClientRepository
{
    Task<ClientEntity?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<Page<ClientEntity>> GetPagedAsync(
        ClientPagedFilter filter,
        CancellationToken cancellationToken = default);

    Task CreateAsync(
        ClientEntity clientEntity,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        ClientEntity clientEntity,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        ClientEntity clientEntity,
        CancellationToken cancellationToken = default);
}
