using JacksonVeroneze.NET.EntityFramework.Interfaces;
using Microsoft.EntityFrameworkCore;

using TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories;
using TaxiService.Modules.Drivers.Domain.Entities;
using TaxiService.Modules.Drivers.Infrastructure.Contexts;

namespace TaxiService.Modules.Drivers.Infrastructure.Repositories.DriverAvailability;

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
}
