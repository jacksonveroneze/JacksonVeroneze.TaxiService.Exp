using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.Bank;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.Bank.Stub;

public class BankWriteStubRepository : IBankWriteRepository
{
    public Task CreateAsync(BankEntity entity,
        CancellationToken cancellationToken = default)
    {
        // BankDatabase.Data.Add(bankEntity);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(BankEntity entity,
        CancellationToken cancellationToken = default)
    {
        // BankDatabase.Data.Remove(bankEntity);

        return Task.CompletedTask;
    }

    public Task UpdateAsync(BankEntity entity,
        CancellationToken cancellationToken = default)
    {
        // BankDatabase.Data.Remove(bankEntity);
        //
        // BankDatabase.Data.Add(bankEntity);

        return Task.CompletedTask;
    }
}
