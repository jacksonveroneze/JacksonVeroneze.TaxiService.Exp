using AutoMapper;
using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;
using Microsoft.Extensions.Logging;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly ILogger<ClientRepository> _logger;
    private readonly IMapper _mapper;

    public ClientRepository(ILogger<ClientRepository> logger,
        IMapper mapper)
    {
        _logger = logger;
        _mapper = mapper;
    }

    public Task<Client?> GetByIdAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Page<Client>> GetPagedAsync(ClientPagedFilter filter,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task CreateAsync(Client client,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Client client,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Client client,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
