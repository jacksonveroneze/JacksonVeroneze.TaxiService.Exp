using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;

public interface IBankWriteRepository
{
    Task CreateAsync(
        BankEntity bankEntity,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        BankEntity bankEntity,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        BankEntity bankEntity,
        CancellationToken cancellationToken = default);
}
