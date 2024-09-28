using System.Linq.Expressions;
using JacksonVeroneze.NET.EntityFramework.Interfaces;
using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.User;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Filters;
using JacksonVeroneze.TaxiService.Exp.Domain.Specifications.Base.Predicate;
using JacksonVeroneze.TaxiService.Exp.Domain.Specifications.User;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.Repositories.User;

[ExcludeFromCodeCoverage]
public class UserReadRepository(
    IEfCoreRepository<UserEntity> service) : IUserReadRepository
{
    public Task<UserEntity?> GetByIdAsync(
        Guid id, CancellationToken cancellationToken = default)
    {
        UserByIdSpecification specByUserId = new(id);

        return service.GetByIdAsync(specByUserId.ToExpression(),
            cancellationToken);
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

        return service.GetPagedAsync(filter.Pagination!,
            spec,
            cancellationToken: cancellationToken);
    }
}