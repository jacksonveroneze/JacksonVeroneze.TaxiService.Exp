using AutoMapper;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.Stubs.Bank;

public class BankWriteStubRepository : IBankWriteRepository
{
    private readonly IList<BankEntity> _data = new List<BankEntity>(Enumerable.Range(1, 25)
        .Select(item => new BankEntity($"Bank_{item}")));

    private readonly ILogger<BankWriteStubRepository> _logger;
    private readonly IMapper _mapper;

    public BankWriteStubRepository(ILogger<BankWriteStubRepository> logger,
        IMapper mapper)
    {
        _logger = logger;
        _mapper = mapper;
    }

    public Task CreateAsync(BankEntity bankEntity,
        CancellationToken cancellationToken = default)
    {
        _data.Add(bankEntity);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(BankEntity bankEntity,
        CancellationToken cancellationToken = default)
    {
        _data.Remove(bankEntity);

        return Task.CompletedTask;
    }

    public Task UpdateAsync(BankEntity bankEntity,
        CancellationToken cancellationToken = default)
    {
        _data.Remove(bankEntity);

        _data.Add(bankEntity);

        return Task.CompletedTask;
    }
}
