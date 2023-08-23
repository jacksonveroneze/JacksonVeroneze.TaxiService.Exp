using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.Bank;

public interface IBankWriteRepository
{
    Task CreateAsync(
        BankEntity entity,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        BankEntity entity,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        BankEntity entity,
        CancellationToken cancellationToken = default);
}
