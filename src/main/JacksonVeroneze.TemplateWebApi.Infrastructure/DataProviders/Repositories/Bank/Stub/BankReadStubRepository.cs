using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;
using JacksonVeroneze.TemplateWebApi.Domain.Specifications;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.Bank.Stub;

public class BankReadStubRepository : IBankReadRepository
{
    public Task<bool> AnyByNameAsync(string name,
        CancellationToken cancellationToken = default)
    {
        BankNameSpecification specName = new(name);

        bool any = BankDatabase.Data
            .Any(specName);

        return Task.FromResult(any);
    }

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
        BankNameSpecification specName =
            new(filter.Name);

        BankStatusSpecification specStatus =
            new(filter.Status ?? BankStatus.None);

        IEnumerable<BankEntity> data = BankDatabase.Data
            .Where(specName)
            .Where(specStatus)
            .Skip(filter.Pagination!.Page - 1)
            .Take(filter.Pagination!.PageSize);

        PageInfo pageInfo = new(
            filter.Pagination!.Page,
            filter.Pagination!.PageSize,
            BankDatabase.Data.Count);

        Page<BankEntity> paged = new(data, pageInfo);

        return Task.FromResult(paged);
    }
}
