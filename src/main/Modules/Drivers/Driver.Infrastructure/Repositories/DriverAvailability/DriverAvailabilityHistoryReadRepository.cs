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
}
