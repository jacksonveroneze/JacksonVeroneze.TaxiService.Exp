using System.Linq.Expressions;
using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.NET.Pagination.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.User;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;
using JacksonVeroneze.TemplateWebApi.Domain.Specifications.Base.Predicate;
using JacksonVeroneze.TemplateWebApi.Domain.Specifications.User;
using JacksonVeroneze.TemplateWebApi.Infrastructure.Contexts;
using JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.User.EntityFramework.Extensions;
using Microsoft.EntityFrameworkCore;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.User.EntityFramework;

[ExcludeFromCodeCoverage]
public class UserReadRepository : IUserReadRepository
{
    private readonly DbSet<UserEntity> _dbSet;

    public UserReadRepository(ApplicationDbContext context)
    {
        _dbSet = context.Set<UserEntity>();
    }

    public Task<bool> ExistsUserAsync(string document,
        CancellationToken cancellationToken = default)
    {
        UserCpfSpecification specName = new(document);

        return _dbSet.AnyAsync(specName, cancellationToken);
    }

    public async Task<UserEntity?> GetByIdAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        UserEntity? result = await _dbSet.FindAsync(
            new object[] { id }, cancellationToken);

        return result;
    }

    public async Task<Page<UserEntity>> GetPagedAsync(
        UserPagedFilter filter,
        CancellationToken cancellationToken = default)
    {
        UserNameSpecification specName = new(filter.Name);
        UserStatusSpecification specStatus = new(filter.Status);

        Expression<Func<UserEntity, bool>> spec =
            specName.ToExpression().And(specStatus);

        int count = await _dbSet
            .AsNoTracking()
            .Where(spec)
            .CountAsync(cancellationToken);

        List<UserEntity> result = await _dbSet
            .AsNoTracking()
            .Where(spec)
            .ConfigurePagination(filter.Pagination!)
            .OrderByDescending(ord => ord.CreatedAt)
            .ToListAsync(cancellationToken);

        Page<UserEntity> data = result
            .ToPage(filter.Pagination!, count);

        return data;
    }
}
