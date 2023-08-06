using AutoMapper;
using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;
using Microsoft.Extensions.Logging;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories;

public class BankRepository : IBankRepository
{
    private readonly ILogger<BankRepository> _logger;
    private readonly IMapper _mapper;

    public BankRepository(ILogger<BankRepository> logger,
        IMapper mapper)
    {
        _logger = logger;
        _mapper = mapper;
    }

    public Task<Bank?> GetByIdAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Page<Bank>> GetPagedAsync(BankPagedFilter filter,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task CreateAsync(Bank bank,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Bank bank,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Bank bank,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
