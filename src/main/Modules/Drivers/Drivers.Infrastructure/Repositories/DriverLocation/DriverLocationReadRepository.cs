using JacksonVeroneze.NET.EntityFramework.Interfaces;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories.DriverLocation;
using TaxiService.Modules.Drivers.Domain.Entities;
using TaxiService.Modules.Drivers.Infrastructure.Contexts;

namespace TaxiService.Modules.Drivers.Infrastructure.Repositories.DriverLocation;

[ExcludeFromCodeCoverage]
public class DriverLocationReadRepository(
    IEfCoreRepository<DriverLocationEntity, DriversDbContext> service)
    : IDriverLocationReadRepository
{
    public Task<DriverLocationEntity?> GetByDriverIdAsync(
        Guid driverId,
        CancellationToken cancellationToken)
    {
        return service.GetSingleOrDefaultAsync(
            conf => conf.DriverId == driverId,
            cancellationToken);
    }
}