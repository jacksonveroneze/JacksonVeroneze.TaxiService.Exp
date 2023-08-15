using System.Linq.Expressions;
using JacksonVeroneze.NET.Extensions.Predicate;
using JacksonVeroneze.NET.MongoDB.Interfaces;
using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;
using JacksonVeroneze.TemplateWebApi.Domain.Specifications;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.Bank;

public class BankReadRepository : IBankReadRepository
{
    private readonly IBaseRepository<BankEntity> _repository;

    public BankReadRepository(
        IBaseRepository<BankEntity> repository)
    {
        _repository = repository;
    }

    public Task<bool> AnyByNameAsync(string name,
        CancellationToken cancellationToken = default)
    {
        BankNameSpecification specName = new(name, matchExactly: true);

        return _repository.AnyAsync(specName, cancellationToken);
    }

    public Task<BankEntity?> GetByIdAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        return _repository.GetByIdAsync(id, cancellationToken);
    }

    public Task<Page<BankEntity>> GetPagedAsync(
        BankPagedFilter filter,
        CancellationToken cancellationToken = default)
    {
        BankNameSpecification specName = new(filter.Name);
        BankStatusSpecification specStatus = new(filter.Status);

        Expression<Func<BankEntity, bool>> spec =
            specName.ToExpression().And(specStatus);

        return _repository.GetPagedAsync(
            filter.Pagination!, spec, cancellationToken);
    }
}
