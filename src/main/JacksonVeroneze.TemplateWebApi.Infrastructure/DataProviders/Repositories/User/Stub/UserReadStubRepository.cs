using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.NET.Pagination.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.User;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;
using JacksonVeroneze.TemplateWebApi.Domain.Specifications.Base.Predicate;
using JacksonVeroneze.TemplateWebApi.Domain.Specifications.User;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.User.Stub;

[ExcludeFromCodeCoverage]
public class UserReadStubRepository : IUserReadRepository
{
    public Task<bool> ExistsAsync(string document,
        CancellationToken cancellationToken = default)
    {
        UserNameSpecification specName = new(document);

        bool exists = UserDatabase.Data
            .Any(specName.IsSatisfiedBy);

        return Task.FromResult(exists);
    }

    public Task<UserEntity?> GetByIdAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        UserEntity? item = UserDatabase.Data
            .FirstOrDefault(item =>
                item.DeletedAt == null && item.Id == id);

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
            .And(specStatus.ToExpression())
            .Compile();

        IList<UserEntity> items = UserDatabase.Data
            .Where(item => item.DeletedAt == null)
            .Where(expression)
            .ToList();

        Page<UserEntity> paged = items
            .ToPageInMemory(filter.Pagination!, items.Count);

        return Task.FromResult(paged);
    }
}
