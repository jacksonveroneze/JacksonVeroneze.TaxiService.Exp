using AutoMapper;
using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;
using Microsoft.Extensions.Logging;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.Client;

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

    public Task<Domain.Entities.Client?> GetByIdAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Page<Domain.Entities.Client>> GetPagedAsync(ClientPagedFilter filter,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task CreateAsync(Domain.Entities.Client client,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Domain.Entities.Client client,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Domain.Entities.Client client,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
