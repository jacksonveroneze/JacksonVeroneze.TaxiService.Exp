using System.Linq.Expressions;

using JacksonVeroneze.NET.EntityFramework.Interfaces;

using Microsoft.Extensions.Logging;

using TaxiService.Common.BaseInfrastructure.Extensions;
using TaxiService.Modules.Drivers.Application.Common;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories;
using TaxiService.Modules.Drivers.Application.v1.Vehicle;
using TaxiService.Modules.Drivers.Application.v1.Vehicle.Common.Models;
using TaxiService.Modules.Drivers.Infrastructure.Contexts;

namespace TaxiService.Modules.Drivers.Infrastructure.Repositories.Vehicle;

[ExcludeFromCodeCoverage]
public class VehicleReadRepository(
    ILogger<VehicleReadRepository> logger,
    IEfCoreRepository<VehicleDto, DriversReadDbContext> service)
    : IVehicleReadRepository
{
    public async Task<bool> ExistsByPlateAsync(
        string plate,
        CancellationToken cancellationToken)
    {
        bool exists = await service.AnyAsync(
            conf => conf.Plate == plate,
            cancellationToken);

        logger.LogExistsByValue(ModuleContext.ContextName,
            nameof(VehicleReadRepository),
            nameof(ExistsByPlateAsync),
            plate, exists);

        return exists;
    }

    public async Task<bool> ExistVehicleByDriverAsync(
        Guid driverId,
        CancellationToken cancellationToken)
    {
        bool exists = await service.AnyAsync(
            conf => conf.DriverId == driverId,
            cancellationToken);

        logger.LogExistsByValue(ModuleContext.ContextName,
            nameof(VehicleReadRepository),
            nameof(ExistVehicleByDriverAsync),
            driverId.ToString(), exists);

        return exists;
    }

    public async Task<VehicleDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        Expression<Func<VehicleDto, bool>> spec
            = entity => entity.Id == id;

        VehicleDto? result = await service
            .GetByIdAsync(spec, cancellationToken);

        logger.LogGetById(ModuleContext.ContextName,
            nameof(VehicleReadRepository),
            nameof(GetByIdAsync),
            id, result != null);

        return result;
    }
}
