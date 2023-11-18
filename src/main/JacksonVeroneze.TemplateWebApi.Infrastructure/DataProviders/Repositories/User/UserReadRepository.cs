using System.Linq.Expressions;
using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.User;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;
using JacksonVeroneze.TemplateWebApi.Domain.Specifications.Base.Predicate;
using JacksonVeroneze.TemplateWebApi.Domain.Specifications.User;
using JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.User;

[ExcludeFromCodeCoverage]
public class UserReadRepository : BaseReadRepository<UserEntity>, IUserReadRepository
{
    private readonly ILogger<UserReadRepository> _logger;
    private readonly DbSet<UserEntity> _dbSet;

    public UserReadRepository(
        ILogger<UserReadRepository> logger,
        DbContext context) : base(logger, context)
    {
        ArgumentNullException.ThrowIfNull(context);

        _logger = logger;
        _dbSet = context.Set<UserEntity>();
    }

    public async Task<bool> ExistsAsync(string document,
        CancellationToken cancellationToken = default)
    {
        UserCpfSpecification specName = new(document);

        bool exists = await _dbSet.AnyAsync(specName,
            cancellationToken);

        return exists;
    }

    public Task<Page<UserEntity>> GetPagedAsync(
        UserPagedFilter filter,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(filter);

        UserNameSpecification specName = new(filter.Name);
        UserStatusSpecification specStatus = new(filter.Status);

        Expression<Func<UserEntity, bool>> spec =
            specName.ToExpression().And(specStatus);

        return GetPagedAsync(spec, filter.Pagination!,
            cancellationToken);
    }
}
