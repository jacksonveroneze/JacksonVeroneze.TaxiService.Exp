using JacksonVeroneze.NET.Extensions.Predicate;
using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.NET.Pagination.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.User;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;
using JacksonVeroneze.TemplateWebApi.Domain.Specifications.User;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.User.Stub;

public class UserReadStubRepository : IUserReadRepository
{
    public Task<bool> AnyByNameAsync(string name,
        CancellationToken cancellationToken = default)
    {
        UserNameSpecification specName = new(name);

        bool any = UserDatabase.Data
            .Any(specName);

        return Task.FromResult(any);
    }

    public Task<UserEntity?> GetByIdAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        UserEntity? item = UserDatabase.Data
            .FirstOrDefault(item => item.Id == id);

        return Task.FromResult(item);
    }

    public Task<Page<UserEntity>> GetPagedAsync(UserPagedFilter filter,
        CancellationToken cancellationToken = default)
    {
        UserNameSpecification specName =
            new(filter.Name);

        UserStatusSpecification specStatus =
            new(filter.Status);

        Func<UserEntity, bool> expression = specName.ToExpression()
            .Or(specStatus.ToExpression())
            .Compile();

        IList<UserEntity> items = UserDatabase.Data
            .Where(expression)
            .ToList();

        Page<UserEntity> paged = items
            .ToPageInMemory(filter.Pagination!, items.Count);

        return Task.FromResult(paged);
    }
}
