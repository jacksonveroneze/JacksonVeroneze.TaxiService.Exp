using System.Linq.Expressions;

using JacksonVeroneze.NET.EntityFramework.Interfaces;
using JacksonVeroneze.NET.Pagination.Cursor;
using JacksonVeroneze.NET.Pagination.Cursor.Extensions;

using Microsoft.Extensions.Logging;

using TaxiService.Common.BaseInfrastructure.Extensions;
using TaxiService.Modules.Rides.Application.Common;
using TaxiService.Modules.Rides.Application.Common.Interfaces.Repositories;
using TaxiService.Modules.Rides.Application.v1.Ride.Common.Filters;
using TaxiService.Modules.Rides.Application.v1.Ride.Common.Models;
using TaxiService.Modules.Rides.Domain.Enums;
using TaxiService.Modules.Rides.Infrastructure.Builders.Filters;
using TaxiService.Modules.Rides.Infrastructure.Contexts;

namespace TaxiService.Modules.Rides.Infrastructure.Repositories.Ride;

[ExcludeFromCodeCoverage]
public class RideReadRepository(
    ILogger<RideReadRepository> logger,
    IEfCoreRepository<RideDto, RidesReadDbContext> service)
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

        logger.LogExistsByValue(ModuleContext.ContextName,
            nameof(RideReadRepository),
            nameof(ExistsActiveByPassengerIdAsync),
            passengerId.ToString(), exists);

        return exists;
    }

    public async Task<RideDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        Expression<Func<RideDto, bool>> spec
            = entity => entity.Id == id;

        RideDto? result = await service
            .GetByIdAsync(spec, cancellationToken);

        logger.LogGetById(ModuleContext.ContextName,
            nameof(RideReadRepository),
            nameof(GetByIdAsync),
            id, result != null);

        return result;
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
