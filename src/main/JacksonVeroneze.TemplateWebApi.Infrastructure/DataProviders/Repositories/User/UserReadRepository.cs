using System.Linq.Expressions;
using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.NET.Pagination.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.User;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;
using JacksonVeroneze.TemplateWebApi.Domain.Specifications.Base.Predicate;
using JacksonVeroneze.TemplateWebApi.Domain.Specifications.User;
using JacksonVeroneze.TemplateWebApi.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.User;

[ExcludeFromCodeCoverage]
public class UserReadRepository : IUserReadRepository
{
    private readonly List<UserEntity> _empty =
        Enumerable.Empty<UserEntity>().ToList();

    private readonly ILogger<UserReadRepository> _logger;
    private readonly DbSet<UserEntity> _dbSet;

    public UserReadRepository(
        ILogger<UserReadRepository> logger,
        DbContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        _logger = logger;
        _dbSet = context.Set<UserEntity>();
    }

    public Task<bool> ExistsAsync(string document,
        CancellationToken cancellationToken = default)
    {
        UserCpfSpecification specName = new(document);

        return _dbSet.AnyAsync(specName,
            cancellationToken);
    }

    public async Task<UserEntity?> GetByIdAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        UserEntity? result = await _dbSet.FindAsync(
            new object[] { id }, cancellationToken);

        if (result is null)
        {
            _logger.LogNotFound(nameof(UserReadRepository),
                nameof(GetByIdAsync), id,
                DomainErrors.User.NotFound);
        }

        return result;
    }

    public async Task<Page<UserEntity>> GetPagedAsync(
        UserPagedFilter filter,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(filter);

        UserNameSpecification specName = new(filter.Name);
        UserStatusSpecification specStatus = new(filter.Status);

        Expression<Func<UserEntity, bool>> spec =
            specName.ToExpression().And(specStatus);

        int count = await _dbSet
            .AsNoTracking()
            .Where(spec)
            .CountAsync(cancellationToken);

        List<UserEntity> result = count == 0
            ? _empty
            : await _dbSet
                .AsNoTracking()
                .Where(spec)
                .ConfigurePagination(filter.Pagination!)
                .OrderByDescending(ord => ord.CreatedAt)
                .ToListAsync(cancellationToken);

        Page<UserEntity> data = result
            .ToPage(filter.Pagination!, count);

        // log

        return data;
    }
}
