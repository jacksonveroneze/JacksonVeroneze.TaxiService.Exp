using System.Linq.Expressions;
using JacksonVeroneze.NET.EntityFramework.Interfaces;
using JacksonVeroneze.NET.Pagination.Cursor;
using JacksonVeroneze.NET.Pagination.Cursor.Extensions;
using TaxiService.Common.BaseDomain.Specifications.Base.Predicate;
using TaxiService.Modules.Rides.Application.v1.Interfaces.Repositories.Ride;
using TaxiService.Modules.Rides.Domain.Entities;
using TaxiService.Modules.Rides.Domain.Filters;
using TaxiService.Modules.Rides.Domain.Specifications.Ride;
using TaxiService.Modules.Rides.Infrastructure.Contexts;

namespace TaxiService.Modules.Rides.Infrastructure.Repositories.Ride;

[ExcludeFromCodeCoverage]
public class RideReadRepository(
    IEfCoreRepository<RideEntity, RidesDbContext> service) : IRideReadRepository
{
    public async Task<bool> ExistsActiveByPassangerIdAsync(
        Guid passangerId,
        CancellationToken cancellationToken)
    {
        RideByPassangerIdSpecification specByPassangerId = new(passangerId);
        RideActivePerPassangerSpecification specActivePerPassangerRequested = new();

        Expression<Func<RideEntity, bool>> spec =
            specByPassangerId.ToExpression().And(specActivePerPassangerRequested.ToExpression());

        bool exists = await service.AnyAsync(spec,
            cancellationToken);

        return exists;
    }

    public Task<RideEntity?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        RideByIdSpecification specByPassangerId = new(id);

        return service.GetByIdAsync(specByPassangerId.ToExpression(),
            cancellationToken);
    }

    public async Task<Page<RideEntity>> GetPagedAsync(
        RidePagedFilter filter,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(filter);

        Guid? id = !string.IsNullOrEmpty(filter.Pagination?.Cursor)
            ? Guid.Parse(filter.Pagination!.Cursor)
            : null;

        RideByCursorSpecification specByCursor = new(id);
        RideByStatusSpecification specByStatus = new(filter.Status);
        RideByPassangerIdSpecification specByPassangerId = new(filter.PassangerId);

        Expression<Func<RideEntity, bool>> spec =
            PredicateExtensions.And(specByCursor.ToExpression(),
                PredicateExtensions.And(specByStatus.ToExpression(), specByPassangerId));

        (RideEntity[] Data, bool HasMore, RideEntity? Last) result =
            await service.GetPagedCursorAsync(
                filter.Pagination!,
                spec,
                order => order.Id,
                cancellationToken);

        string? cursor = result.HasMore
            ? result.Last?.Id.ToString()
            : null;

        return result.Data.ToPage(result.HasMore, cursor);
    }
}