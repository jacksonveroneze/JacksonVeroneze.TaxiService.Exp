using JacksonVeroneze.NET.EntityFramework.Interfaces;
using Microsoft.Extensions.Logging;
using TaxiService.Common.BaseInfrastructure.Extensions;
using TaxiService.Modules.Driver.Application.Common.Interfaces.Repositories;
using TaxiService.Modules.Driver.Domain;
using TaxiService.Modules.Driver.Domain.Entities;
using TaxiService.Modules.Driver.Infrastructure.Contexts;

namespace TaxiService.Modules.Driver.Infrastructure.Repositories.DriverLocation;

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

        logger.LogExistsByValue(
            ModuleConstants.ContextName,
            nameof(DriverLocationHistoryReadRepository),
            nameof(GetByDriverIdAsync),
            driverId.ToString(), result is not null);

        return result;
    }
}
