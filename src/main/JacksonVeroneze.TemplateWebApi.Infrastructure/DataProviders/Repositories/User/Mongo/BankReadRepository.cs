using System.Linq.Expressions;
using JacksonVeroneze.NET.Extensions.Predicate;
using JacksonVeroneze.NET.MongoDB.Interfaces;
using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.User;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;
using JacksonVeroneze.TemplateWebApi.Domain.Specifications.User;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.User.Mongo;

public class UserReadRepository : IUserReadRepository
{
    private readonly IBaseRepository<UserEntity, Guid> _repository;

    public UserReadRepository(
        IBaseRepository<UserEntity, Guid> repository)
    {
        _repository = repository;
    }

    public Task<bool> AnyByNameAsync(string name,
        CancellationToken cancellationToken = default)
    {
        UserNameSpecification specName = new(name, matchExactly: true);

        return _repository.AnyAsync(specName, cancellationToken);
    }

    public Task<UserEntity?> GetByIdAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        return _repository.GetByIdAsync(id, cancellationToken);
    }

    public Task<Page<UserEntity>> GetPagedAsync(
        UserPagedFilter filter,
        CancellationToken cancellationToken = default)
    {
        UserNameSpecification specName = new(filter.Name);
        UserStatusSpecification specStatus = new(filter.Status);

        Expression<Func<UserEntity, bool>> spec =
            specName.ToExpression().And(specStatus);

        return _repository.GetPagedAsync(
            filter.Pagination!, spec, cancellationToken);
    }
}
