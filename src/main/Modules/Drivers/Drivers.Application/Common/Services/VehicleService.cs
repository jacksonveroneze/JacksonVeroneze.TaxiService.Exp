using JacksonVeroneze.NET.Result;

using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories.Vehicle;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Services;
using TaxiService.Modules.Drivers.Application.v1.Vehicle.Common.Models;
using TaxiService.Modules.Drivers.Domain;
using TaxiService.Modules.Drivers.Domain.Core.Errors;
using TaxiService.Modules.Drivers.Domain.Entities;

namespace TaxiService.Modules.Drivers.Application.Common.Services;

public class VehicleService(
    ILogger<VehicleService> logger,
    IVehicleRepository vehicleRepository,
    IVehicleReadRepository vehicleReadRepository)
    : IVehicleService
{
    public Task<Result<VehicleEntity>> GetEntityByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return InternalGetByIdAsync(id, cancellationToken);
    }

    public async Task<Result<VehicleDto>> GetDtoByIdAsync(
        Guid id, CancellationToken cancellationToken)
    {
        VehicleDto? dto = await vehicleReadRepository
            .GetByIdAsync(id, cancellationToken);
        
        if (dto is not null)
        {
            logger.LogGetById(
                ContextInfo.ContextName,
                nameof(VehicleService),
                nameof(GetDtoByIdAsync), id);

            return Result<VehicleDto>.WithSuccess(dto);
        }

        Error error = DomainErrors.VehicleErrors.NotFound;

        logger.LogNotFound(
            nameof(VehicleService),
            nameof(GetDtoByIdAsync), id, error);

        return Result<VehicleDto>.FromNotFound(error);
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
            nameof(GetEntityByIdAsync), id, error);

        return Result<VehicleEntity>.FromNotFound(error);
    }
}
