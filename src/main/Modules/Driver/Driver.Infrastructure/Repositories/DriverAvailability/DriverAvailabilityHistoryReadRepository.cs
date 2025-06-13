using JacksonVeroneze.NET.EntityFramework.Interfaces;
using Microsoft.EntityFrameworkCore;
using TaxiService.Modules.Driver.Application.Common.Interfaces.Repositories;
using TaxiService.Modules.Driver.Domain.Entities;
using TaxiService.Modules.Driver.Infrastructure.Contexts;

namespace TaxiService.Modules.Driver.Infrastructure.Repositories.DriverAvailability;

[ExcludeFromCodeCoverage]
public class DriverAvailabilityHistoryReadRepository(
    IEfCoreRepository<DriverAvailabilityHistory, DriversDbContext> service)
    : IDriverAvailabilityHistoryReadRepository
{
    public Task<DriverAvailabilityHistory?> GetLastByDriverIdAsync(
        Guid driverId,
        CancellationToken cancellationToken)
    {
        return service.DbSet
            .OrderByDescending(order => order.CreatedAt)
            .FirstOrDefaultAsync(conf => conf.DriverId == driverId, cancellationToken);
    }

    public async Task<ICollection<DriverAvailabilityHistory>> GetLastByDriverIdsAsync(
        Guid[] driverIds,
        CancellationToken cancellationToken)
    {
        DriverAvailabilityHistory?[] result = await service.DbSet
            .Where(conf => driverIds.Contains(conf.DriverId))
            .GroupBy(group => group.DriverId)
            .Select(item => item.OrderByDescending(order => order.CreatedAt)
                .FirstOrDefault())
            .ToArrayAsync(cancellationToken);

        return result!;
    }
}
