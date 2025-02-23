using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.System;
using TaxiService.Modules.Drivers.Application.v1.Commands.Driver;
using TaxiService.Modules.Drivers.Application.v1.Interfaces.Repositories.Driver;
using TaxiService.Modules.Drivers.Application.v1.Interfaces.Repositories.DriverAvailability;
using TaxiService.Modules.Drivers.Application.v1.Interfaces.Services;
using TaxiService.Modules.Drivers.Domain.Core.Errors;
using TaxiService.Modules.Drivers.Domain.Entities;
using TaxiService.Modules.Drivers.Domain.Enums;

namespace TaxiService.Modules.Drivers.Application.v1.Handlers.CommandHandler.Driver;

public sealed class DeleteCommandHandler(
    ILogger<DeleteCommandHandler> logger,
    IDriverAvailabilityReadRepository driverAvailabilityReadRepository,
    IDriverService driverService,
    IDriverWriteRepository driverWriteRepository)
    : IRequestHandler<DeleteCommand, Result>
{
    public async Task<Result> Handle(
        DeleteCommand request,
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

        DriverAvailabilityEntity? result = await driverAvailabilityReadRepository
            .GetByDriverIdAsync(request.Id, cancellationToken);

        if (result?.Status == DriverAvailability.InRide)
        {
            Error error = DomainErrors.DriverErrors.DriverHasActiveRide;

            logger.LogGenericValidationError(
                nameof(DeleteCommandHandler),
                nameof(Handle), error);

            return Result<DriverEntity>.WithError(error);
        }

        Result resultAction = Remove(resultEntity.Value!);

        if (resultAction.IsFailure)
        {
            return resultAction;
        }

        await DeleteAsync(resultEntity.Value!, cancellationToken);

        return Result.WithSuccess();
    }

    private Result Remove(DriverEntity entity)
    {
        Result result = entity.Remove();

        if (result.IsFailure)
        {
            logger.LogAlreadyProcessed(
                nameof(ActivateCommandHandler),
                nameof(Remove), entity.Id, result.Error!);
        }

        return result;
    }

    private async Task DeleteAsync(
        DriverEntity driver,
        CancellationToken cancellationToken)
    {
        await driverWriteRepository.DeleteAsync(
            driver, cancellationToken);

        logger.LogDeleted(nameof(DeleteCommandHandler),
            nameof(Handle), driver.Id);
    }
}