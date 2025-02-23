using JacksonVeroneze.NET.EntityFramework.Interfaces;
using TaxiService.Modules.Drivers.Application.v1.Interfaces.Repositories.DriverAvailability;
using TaxiService.Modules.Drivers.Domain.Entities;
using TaxiService.Modules.Drivers.Infrastructure.Contexts;

namespace TaxiService.Modules.Drivers.Infrastructure.Repositories.DriverAvailability;

[ExcludeFromCodeCoverage]
public class DriverAvailabilityReadRepository(
    IEfCoreRepository<DriverAvailabilityEntity, DriversDbContext> service)
    : IDriverAvailabilityReadRepository
{
    public Task<DriverAvailabilityEntity?> GetByDriverIdAsync(
        Guid driverId,
        CancellationToken cancellationToken)
    {
        return service.GetSingleOrDefaultAsync(
            conf => conf.DriverId == driverId,
            cancellationToken);
    }
}