using System.Linq.Expressions;
using JacksonVeroneze.NET.EntityFramework.Interfaces;
using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Ride;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Filters;
using JacksonVeroneze.TaxiService.Exp.Domain.Results;
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

        return service.GetPagedAsync(
            filter.Pagination!,
            spec,
            order => order.Id,
            cancellationToken);
    }

    public async Task<PaginatedResult<RideEntity>> GetCursorPagedAsync(
        RidePagedFilter filter,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(filter);

        Guid? id = !string.IsNullOrEmpty(filter.CursorPagination?.Cursor)
            ? Guid.Parse(filter.CursorPagination!.Cursor)
            : null;

        RideByCursorSpecification specByCursor = new(id);
        RideByStatusSpecification specByStatus = new(filter.Status);
        RideByUserIdSpecification specByUserId = new(filter.UserId);

        Expression<Func<RideEntity, bool>> spec =
            specByCursor.ToExpression()
                .And(specByStatus.ToExpression()
                    .And(specByUserId));

        int paginationLimit = filter.CursorPagination!.Limit!.Value;
        int databaselimit = paginationLimit + 1;

        ICollection<RideEntity> result = await service.GetAllAsync(
            spec,
            order => order.Status,
            databaselimit,
            cancellationToken: cancellationToken);

        ICollection<RideEntity> data = result.Take(paginationLimit).ToArray();
        bool hasMore = result.Count > paginationLimit;
        string? cursor = hasMore ? data.LastOrDefault()?.Id.ToString() : null;

        return new PaginatedResult<RideEntity>(data, hasMore, cursor);
    }
}