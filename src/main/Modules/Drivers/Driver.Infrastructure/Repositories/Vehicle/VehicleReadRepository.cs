using System.Linq.Expressions;
using JacksonVeroneze.NET.EntityFramework.Interfaces;
using Microsoft.Extensions.Logging;
using TaxiService.Common.BaseInfrastructure.Extensions;
using TaxiService.Modules.Driver.Application.Common;
using TaxiService.Modules.Driver.Application.Common.Interfaces.Repositories;
using TaxiService.Modules.Driver.Application.v1.Vehicle.Common.Models;
using TaxiService.Modules.Driver.Infrastructure.Contexts;

namespace TaxiService.Modules.Driver.Infrastructure.Repositories.Vehicle;

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
