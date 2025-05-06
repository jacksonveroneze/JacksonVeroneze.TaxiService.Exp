using JacksonVeroneze.NET.Result;

using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories.Vehicle;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Services;
using TaxiService.Modules.Drivers.Domain;
using TaxiService.Modules.Drivers.Domain.Core.Errors;
using TaxiService.Modules.Drivers.Domain.Entities;

namespace TaxiService.Modules.Drivers.Application.Common.Services;

public class VehicleService(
    ILogger<VehicleService> logger,
    IVehicleRepository vehicleRepository)
    : IVehicleService
{
    public Task<Result<VehicleEntity>> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return InternalGetByIdAsync(id, cancellationToken);
    }

    private async Task<Result<VehicleEntity>> InternalGetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        VehicleEntity? entity = await vehicleRepository
            .GetByIdAsync(id, cancellationToken);

        if (entity is not null)
        {
            logger.LogGetById(
                ContextInfo.ContextName,
                nameof(VehicleService),
                nameof(InternalGetByIdAsync), id);

            return Result<VehicleEntity>.WithSuccess(entity);
        }

        Error error = DomainErrors.VehicleErrors.NotFound;

        logger.LogNotFound(
            nameof(VehicleService),
            nameof(GetByIdAsync), id, error);

        return Result<VehicleEntity>.FromNotFound(error);
    }
}
