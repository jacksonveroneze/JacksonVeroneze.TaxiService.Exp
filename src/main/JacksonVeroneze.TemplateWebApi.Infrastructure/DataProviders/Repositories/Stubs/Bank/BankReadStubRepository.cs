using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.Stubs.Bank;

public class BankReadStubRepository : IBankReadRepository
{
    public Task<BankEntity?> GetByIdAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        BankEntity? item = BankDatabase.Data
            .FirstOrDefault(item => item.Id == id);

        return Task.FromResult(item);
    }

    public Task<Page<BankEntity>> GetPagedAsync(BankPagedFilter filter,
        CancellationToken cancellationToken = default)
    {
        PageInfo pageInfo = new(
            filter.Pagination!.Page,
            filter.Pagination!.PageSize,
            BankDatabase.Data.Count);

        Page<BankEntity> paged = new(BankDatabase.Data, pageInfo);

        return Task.FromResult(paged);
    }
}
