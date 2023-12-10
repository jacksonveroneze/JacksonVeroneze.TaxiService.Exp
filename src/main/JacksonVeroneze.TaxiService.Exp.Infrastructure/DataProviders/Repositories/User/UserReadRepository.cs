using System.Linq.Expressions;
using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.User;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Filters;
using JacksonVeroneze.TaxiService.Exp.Domain.Specifications.Base.Predicate;
using JacksonVeroneze.TaxiService.Exp.Domain.Specifications.User;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.Contexts;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.DataProviders.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.DataProviders.Repositories.User;

[ExcludeFromCodeCoverage]
public class UserReadRepository :
    BaseReadRepository<UserEntity>, IUserReadRepository
{
    private readonly DbSet<UserEntity> _dbSet;

    public UserReadRepository(
        ILogger<UserReadRepository> logger,
        ApplicationDbContext context) : base(logger, context)
    {
        ArgumentNullException.ThrowIfNull(context);

        _dbSet = context.Set<UserEntity>();
    }

    public async Task<bool> ExistsByEmailAsync(string email,
        CancellationToken cancellationToken = default)
    {
        UserEmailSpecification specName = new(email);

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
