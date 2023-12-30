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
    public UserReadRepository(
        ILogger<UserReadRepository> logger,
        ApplicationDbContext context) : base(logger, context)
    {
        ArgumentNullException.ThrowIfNull(context);
    }

    public Task<Page<UserEntity>> GetPagedAsync(
        UserPagedFilter filter,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(filter);

        UserByNameSpecification specByName = new(filter.Name);
        UserByStatusSpecification specByStatus = new(filter.Status);

        Expression<Func<UserEntity, bool>> spec =
            specByName.ToExpression().And(specByStatus);

        return GetPagedAsync(spec, filter.Pagination!,
            cancellationToken);
    }
}