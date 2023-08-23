using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;

namespace JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.Client;

public interface IClientReadRepository
{
    Task<bool> AnyByNameAsync(
        string name,
        CancellationToken cancellationToken = default);

    Task<ClientEntity?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<Page<ClientEntity>> GetPagedAsync(
        ClientPagedFilter filter,
        CancellationToken cancellationToken = default);
}
