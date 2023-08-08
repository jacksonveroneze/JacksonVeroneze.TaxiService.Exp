using AutoMapper;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.Bank;

public class BankWriteRepository : IBankWriteRepository
{
    private readonly ILogger<BankWriteRepository> _logger;
    private readonly IMapper _mapper;

    public BankWriteRepository(ILogger<BankWriteRepository> logger,
        IMapper mapper)
    {
        _logger = logger;
        _mapper = mapper;
    }

    public Task CreateAsync(Domain.Entities.Bank bank,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Domain.Entities.Bank bank,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Domain.Entities.Bank bank,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
