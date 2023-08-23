using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.NET.Pagination.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.Client;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;
using JacksonVeroneze.TemplateWebApi.Domain.Specifications.Client;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.Client.Stub;

public class ClientReadStubRepository : IClientReadRepository
{
    public Task<bool> AnyByNameAsync(string name,
        CancellationToken cancellationToken = default)
    {
        ClientNameSpecification specName = new(name);

        bool any = ClientDatabase.Data
            .Any(specName);

        return Task.FromResult(any);
    }

    public Task<ClientEntity?> GetByIdAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        ClientEntity? item = ClientDatabase.Data
            .FirstOrDefault(item => item.Id == id);

        return Task.FromResult(item);
    }

    public Task<Page<ClientEntity>> GetPagedAsync(ClientPagedFilter filter,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(filter);

        ClientNameSpecification specName =
            new(filter.Name);

        IList<ClientEntity> items = ClientDatabase.Data
            .Where(specName)
            .ToList();

        Page<ClientEntity> paged = items
            .ToPageInMemory(filter.Pagination!, items.Count);

        return Task.FromResult(paged);
    }
}
