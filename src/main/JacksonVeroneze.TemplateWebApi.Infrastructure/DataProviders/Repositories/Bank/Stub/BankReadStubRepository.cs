using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.NET.Pagination.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;
using JacksonVeroneze.TemplateWebApi.Domain.Specifications;
using JacksonVeroneze.TemplateWebApi.Domain.Specifications.Bank;

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

        IList<BankEntity> items = BankDatabase.Data
            .Where(specName)
            .ToList();

        Page<BankEntity> paged = items
            .ToPageInMemory(filter.Pagination!, items.Count);

        return Task.FromResult(paged);
    }
}
