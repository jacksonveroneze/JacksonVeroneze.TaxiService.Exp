using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.Client;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;
using JacksonVeroneze.TemplateWebApi.Domain.Specifications;
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
        ClientNameSpecification specName =
            new(filter.Name);

        IEnumerable<ClientEntity> data = ClientDatabase.Data
            .Where(specName)
            .Skip(filter.Pagination!.Page - 1)
            .Take(filter.Pagination!.PageSize);

        PageInfo pageInfo = new(
            filter.Pagination!.Page,
            filter.Pagination!.PageSize,
            ClientDatabase.Data.Count);

        Page<ClientEntity> paged = new(data, pageInfo);

        return Task.FromResult(paged);
    }
}
