using System.Linq.Expressions;

using JacksonVeroneze.NET.EntityFramework.Interfaces;
using JacksonVeroneze.NET.Pagination.Cursor;
using JacksonVeroneze.NET.Pagination.Cursor.Extensions;

using TaxiService.Modules.Rides.Application.v1.Interfaces.Repositories.Ride;
using TaxiService.Modules.Rides.Application.v1.Models.Ride;
using TaxiService.Modules.Rides.Domain.Enums;
using TaxiService.Modules.Rides.Domain.Filters;
using TaxiService.Modules.Rides.Infrastructure.Builders.Filters;
using TaxiService.Modules.Rides.Infrastructure.Contexts;

namespace TaxiService.Modules.Rides.Infrastructure.Repositories.Ride;

[ExcludeFromCodeCoverage]
public class RideReadRepository(
    IEfCoreRepository<RideDto, RidesDbContext> service)
    : IRideReadRepository
{
    public async Task<bool> ExistsActiveByPassengerIdAsync(
        Guid passengerId, 
        CancellationToken cancellationToken)
    {
        RideStatus[] activityStatus =
        [
            RideStatus.Requested,
            RideStatus.Accepted,
            RideStatus.InProgress,
        ];
        
        Expression<Func<RideDto, bool>> spec
            = entity => entity.PassengerId == passengerId && 
                        activityStatus.Contains(entity.Status!.Value);

        bool exists = await service
            .AnyAsync(spec, cancellationToken);

        return exists;
    }

    public Task<RideDto?> GetByIdAsync(
        Guid id, 
        CancellationToken cancellationToken)
    {
        Expression<Func<RideDto, bool>> spec
            = entity => entity.Id == id;

        return service.GetByIdAsync(spec,
            cancellationToken);
    }

    public async Task<Page<RideDto>> GetPagedAsync(
        RidePagedFilter filter, 
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(filter);

        Expression<Func<RideDto, bool>> expression =
            RidePagedFilterBuilder.Create(filter).Build();

        int pageLimit = filter.Pagination!.Limit!.Value;
        int databaselimit = pageLimit + 1;

        ICollection<RideDto> result =
            await service.GetAllAsync(
                expression,
                order => order.Id,
                databaselimit,
                cancellationToken: cancellationToken);

        return result.ToPage(
            filter.Pagination, d => d.Id.ToString());
    }
}
