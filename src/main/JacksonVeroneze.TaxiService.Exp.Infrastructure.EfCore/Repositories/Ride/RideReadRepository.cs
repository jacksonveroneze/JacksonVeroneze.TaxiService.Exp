using System.Linq.Expressions;
using JacksonVeroneze.NET.EntityFramework.Interfaces;
using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Ride;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Filters;
using JacksonVeroneze.TaxiService.Exp.Domain.Specifications.Base.Predicate;
using JacksonVeroneze.TaxiService.Exp.Domain.Specifications.Ride;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.Repositories.Ride;

[ExcludeFromCodeCoverage]
public class RideReadRepository(
    IEfCoreRepository<RideEntity> service) : IRideReadRepository
{
    public async Task<bool> ExistsActiveByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        RideByUserIdSpecification specByUserId = new(userId);
        RideActivePerUserSpecification specActivePerUserRequested = new();

        Expression<Func<RideEntity, bool>> spec =
            specByUserId.ToExpression()
                .And(specActivePerUserRequested.ToExpression());

        bool exists = await service.AnyAsync(spec,
            cancellationToken);

        return exists;
    }

    public Task<RideEntity?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        RideByIdSpecification specByUserId = new(id);

        return service.GetByIdAsync(specByUserId.ToExpression(),
            cancellationToken);
    }

    public Task<Page<RideEntity>> GetPagedAsync(
        RidePagedFilter filter,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(filter);

        RideByStatusSpecification specByStatus = new(filter.Status);
        RideByUserIdSpecification specByUserId = new(filter.UserId);

        Expression<Func<RideEntity, bool>> spec =
            specByStatus.ToExpression().And(specByUserId);

        return service.GetPagedAsync(filter.Pagination!, spec,
            cancellationToken: cancellationToken);
    }
}