using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.Bank.Stub;

public class BankWriteStubRepository : IBankWriteRepository
{
    public Task CreateAsync(BankEntity bankEntity,
        CancellationToken cancellationToken = default)
    {
        BankDatabase.Data.Add(bankEntity);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(BankEntity bankEntity,
        CancellationToken cancellationToken = default)
    {
        BankDatabase.Data.Remove(bankEntity);

        return Task.CompletedTask;
    }

    public Task UpdateAsync(BankEntity bankEntity,
        CancellationToken cancellationToken = default)
    {
        BankDatabase.Data.Remove(bankEntity);

        BankDatabase.Data.Add(bankEntity);

        return Task.CompletedTask;
    }
}
