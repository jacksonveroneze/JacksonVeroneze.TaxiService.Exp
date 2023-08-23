using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;

namespace JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.Bank;

public interface IBankReadRepository
{
    Task<bool> AnyByNameAsync(
        string name,
        CancellationToken cancellationToken = default);

    Task<BankEntity?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<Page<BankEntity>> GetPagedAsync(
        BankPagedFilter filter,
        CancellationToken cancellationToken = default);
}