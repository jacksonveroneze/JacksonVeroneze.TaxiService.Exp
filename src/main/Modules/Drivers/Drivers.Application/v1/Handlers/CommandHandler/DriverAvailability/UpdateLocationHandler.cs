using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Drivers.Application.v1.Commands.DriverAvailability;
using TaxiService.Modules.Drivers.Application.v1.Interfaces.Repositories.DriverAvailability;
using TaxiService.Modules.Drivers.Application.v1.Interfaces.Services;
using TaxiService.Modules.Drivers.Domain.Entities;

namespace TaxiService.Modules.Drivers.Application.v1.Handlers.CommandHandler.DriverAvailability;

public sealed class UpdateLocationHandler(
    ILogger<UpdateLocationHandler> logger,
    IDriverService driverService,
    IDriverAvailabilityReadRepository driverAvailabilityReadRepository,
    IDriverAvailabilityWriteRepository driverAvailabilityWriteRepository)
    : IRequestHandler<UpdateLocationCommand, Result>
{
    public async Task<Result> Handle(
        UpdateLocationCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result<DriverEntity> resultEntity =
            await driverService.GetByIdAsync(
                request.Id, cancellationToken);

        if (resultEntity.IsFailure)
        {
            return resultEntity;
        }

        DriverAvailabilityEntity? availability =
            await driverAvailabilityReadRepository
                .GetByDriverIdAsync(request.Id, cancellationToken);

        Result resultAction = Update(availability!, request);

        if (resultAction.IsFailure)
        {
            return resultAction;
        }

        await UpdateAsync(availability!, cancellationToken);

        return Result.WithSuccess();
    }

    private Result Update(
        DriverAvailabilityEntity entity,
        UpdateLocationCommand request)
    {
        var result = entity.UpdateLocation(
            request.Latitude, request.Longitude);

        if (result.IsFailure)
        {
            logger.LogGenericValidationError(
                nameof(UpdateLocationHandler),
                nameof(Update), entity.Id, result.Error!);
        }

        return result;
    }
    
    private async Task UpdateAsync(
        DriverAvailabilityEntity entity,
        CancellationToken cancellationToken)
    {
        await driverAvailabilityWriteRepository.UpdateAsync(
            entity, cancellationToken);

        logger.LogProcessed(
            nameof(UpdateLocationHandler),
            nameof(UpdateAsync), entity.Id);
    }
}