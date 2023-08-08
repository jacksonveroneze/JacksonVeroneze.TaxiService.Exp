using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;

public interface IBankWriteRepository
{
    Task CreateAsync(
        Bank bank,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        Bank bank,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        Bank bank,
        CancellationToken cancellationToken = default);
}
