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
    IDriverRepository driverRepository)
    : IRequestHandler<DeleteDriverCommand, Result>
{
    public async Task<Result> Handle(
        DeleteDriverCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        DriverEntity? entity = await driverRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            return Result.FromNotFound(
                DomainErrors.DriverErrors.NotFound);
        }
        
        Result validationResult = await ValidateBussinesRulesAsync(
            request, cancellationToken);

        if (validationResult.IsFailure)
        {
            return validationResult;
        }

        Result resultAction = Remove(entity);

        if (resultAction.IsFailure)
        {
            return resultAction;
        }

        await DeleteAsync(entity, cancellationToken);

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

        return Result.FromRuleViolation(error);
    }

    private Result Remove(DriverEntity entity)
    {
        Result result = entity.Remove();

        if (result.IsFailure)
        {
            logger.LogGenericValidationError(
                nameof(ActivateDriverCommandHandler),
                nameof(Remove), entity.Id, result.FirstError!);
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
