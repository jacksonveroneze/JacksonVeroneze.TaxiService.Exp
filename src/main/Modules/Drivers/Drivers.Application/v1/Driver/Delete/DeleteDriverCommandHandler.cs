using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.Messaging;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories;
using TaxiService.Modules.Drivers.Application.v1.Driver.Activate;
using TaxiService.Modules.Drivers.Domain.Aggregates;
using TaxiService.Modules.Drivers.Domain.Core.Errors;
using TaxiService.Modules.Drivers.Domain.Entities;
using TaxiService.Modules.Drivers.Domain.Enums;
using TaxiService.Modules.Drivers.Domain.Repositories;

namespace TaxiService.Modules.Drivers.Application.v1.Driver.Delete;

public sealed class DeleteDriverCommandHandler(
    ILogger<DeleteDriverCommandHandler> logger,
    IDriverAvailabilityHistoryReadRepository driverAvailabilityHistoryReadRepository,
    IDriverRepository driverRepository,
    IDomainEventDispatcher eventDispatcher)
    : IRequestHandler<DeleteDriverCommand, Result>
{
    public async Task<Result> Handle(
        DeleteDriverCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Domain.Aggregates.Driver? entity = await driverRepository
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

        await driverRepository.DeleteAsync(
            entity, cancellationToken);

        await eventDispatcher.DispatchAsync(entity);
        
        logger.LogDeleted(nameof(DeleteDriverCommandHandler),
            nameof(Handle), entity.Id);

        return Result.WithSuccess();
    }
    
    private async Task<Result> ValidateBussinesRulesAsync(
        DeleteDriverCommand request,
        CancellationToken cancellationToken)
    {
        DriverAvailabilityHistory? result = await driverAvailabilityHistoryReadRepository
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

    private Result Remove(Domain.Aggregates.Driver entity)
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
}
