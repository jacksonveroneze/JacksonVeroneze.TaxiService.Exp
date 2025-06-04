using JacksonVeroneze.NET.EntityFramework.Interfaces;
using Microsoft.Extensions.Logging;
using TaxiService.Common.BaseInfrastructure.Extensions;
using TaxiService.Modules.Drivers.Application.Common;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories;
using TaxiService.Modules.Drivers.Domain.Entities;
using TaxiService.Modules.Drivers.Infrastructure.Contexts;

namespace TaxiService.Modules.Drivers.Infrastructure.Repositories.DriverLocation;

[ExcludeFromCodeCoverage]
public class DriverLocationHistoryReadRepository(
    ILogger<DriverLocationHistoryReadRepository> logger,
    IEfCoreRepository<DriverLocationHistory, DriversDbContext> service)
    : IDriverLocationHistoryReadRepository
{
    public async Task<DriverLocationHistory?> GetByDriverIdAsync(
        Guid driverId,
        CancellationToken cancellationToken)
    {
        DriverLocationHistory? result = await service
            .GetSingleOrDefaultAsync(
                conf => conf.DriverId == driverId,
                cancellationToken);

        logger.LogExistsByValue(ModuleContext.ContextName,
            nameof(DriverLocationHistoryReadRepository),
            nameof(GetByDriverIdAsync),
            driverId.ToString(), result is not null);

        return result;
    }
}
