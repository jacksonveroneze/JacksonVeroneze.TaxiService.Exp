using AutoMapper;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.Stubs.Bank;

public class BankWriteStubRepository : IBankWriteRepository
{
    private readonly IList<Domain.Entities.Bank> _data = Enumerable.Range(1, 25)
        .Select(item => new Domain.Entities.Bank($"Bank_{item}"))
        .ToArray();

    private readonly ILogger<BankWriteStubRepository> _logger;
    private readonly IMapper _mapper;

    public BankWriteStubRepository(ILogger<BankWriteStubRepository> logger,
        IMapper mapper)
    {
        _logger = logger;
        _mapper = mapper;
    }

    public Task CreateAsync(Domain.Entities.Bank bank,
        CancellationToken cancellationToken = default)
    {
        _data.Add(bank);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(Domain.Entities.Bank bank,
        CancellationToken cancellationToken = default)
    {
        _data.Remove(bank);

        return Task.CompletedTask;
    }

    public Task UpdateAsync(Domain.Entities.Bank bank,
        CancellationToken cancellationToken = default)
    {
        _data.Remove(bank);

        _data.Add(bank);

        return Task.CompletedTask;
    }
}
