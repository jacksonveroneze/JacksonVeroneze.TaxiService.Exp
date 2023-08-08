using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;

namespace JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;

public interface IBankReadRepository
{
    Task<Bank?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<Page<Bank>> GetPagedAsync(
        BankPagedFilter filter,
        CancellationToken cancellationToken = default);
}
