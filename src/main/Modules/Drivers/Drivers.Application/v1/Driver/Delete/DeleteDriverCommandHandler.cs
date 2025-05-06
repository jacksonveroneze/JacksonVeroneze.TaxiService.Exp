using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories.Driver;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories.DriverAvailability;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Services;
using TaxiService.Modules.Drivers.Application.v1.Driver.Activate;
using TaxiService.Modules.Drivers.Domain.Core.Errors;
using TaxiService.Modules.Drivers.Domain.Entities;
using TaxiService.Modules.Drivers.Domain.Enums;

namespace TaxiService.Modules.Drivers.Application.v1.Driver.Delete;

public sealed class DeleteDriverCommandHandler(
    ILogger<DeleteDriverCommandHandler> logger,
    IDriverAvailabilityReadRepository driverAvailabilityReadRepository,
    IDriverService driverService,
    IDriverRepository driverRepository)
    : IRequestHandler<DeleteDriverCommand, Result>
{
    public async Task<Result> Handle(
        DeleteDriverCommand request,
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
        
        Result validationResult = await ValidateBussinesRulesAsync(
            request, cancellationToken);

        if (validationResult.IsFailure)
        {
            return validationResult;
        }

        Result resultAction = Remove(resultEntity.Value!);

        if (resultAction.IsFailure)
        {
            return resultAction;
        }

        await DeleteAsync(resultEntity.Value!, cancellationToken);

        return Result.WithSuccess();
    }
    
    private async Task<Result> ValidateBussinesRulesAsync(
        DeleteDriverCommand request,
        CancellationToken cancellationToken)
    {
        DriverAvailabilityEntity? result = await driverAvailabilityReadRepository
            .GetLastByDriverIdAsync(request.Id, cancellationToken);

        if (result?.Status != DriverAvailabilityStatus.InRide)
        {
            return Result.WithSuccess();
        }

        Error error = DomainErrors.DriverErrors.DriverHasActiveRide;

        logger.LogGenericValidationError(
            nameof(DeleteDriverCommandHandler),
            nameof(Handle), error);

        return Result.WithError(error);
    }

    private Result Remove(DriverEntity entity)
    {
        Result result = entity.Remove();

        if (result.IsFailure)
        {
            logger.LogGenericValidationError(
                nameof(ActivateDriverCommandHandler),
                nameof(Remove), entity.Id, result.Error!);
        }

        return result;
    }

    private async Task DeleteAsync(
        DriverEntity driver,
        CancellationToken cancellationToken)
    {
        await driverRepository.DeleteAsync(
            driver, cancellationToken);

        logger.LogDeleted(nameof(DeleteDriverCommandHandler),
            nameof(Handle), driver.Id);
    }
}
