using JacksonVeroneze.NET.Result;
using RedLockNet;
using TaxiService.Common.BaseApplication.Core.Errors;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.Messaging;
using TaxiService.Modules.Driver.Domain.Enums;
using TaxiService.Modules.Driver.PublicApi.v1.Interfaces;
using TaxiService.Modules.Driver.PublicApi.v1.Models;
using TaxiService.Modules.Ride.Application.Common.Interfaces;
using TaxiService.Modules.Ride.Application.Common.Interfaces.Repositories;
using TaxiService.Modules.Ride.Domain;
using TaxiService.Modules.Ride.Domain.Core.Errors;
using TaxiService.Modules.Ride.Domain.Enums;
using TaxiService.Modules.Ride.Domain.Repositories;
using TaxiService.Modules.Ride.Domain.Services;

namespace TaxiService.Modules.Ride.Application.v1.Ride.Accept;

public sealed class AcceptRideCommandHandler(
    ILogger<AcceptRideCommandHandler> logger,
    IDriverPublicApi driverPublicApi,
    IRideRepository rideRepository,
    IRideDistributedLockRepository distributedLockRepository,
    IRideStatusTransitionValidator rideStatusTransitionValidator,
    IDomainEventDispatcher eventDispatcher,
    IMetricsPublisher metricsPublisher)
    : IRequestHandler<AcceptRideCommand, Result>
{
    public async Task<Result> Handle(
        AcceptRideCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        await using IRedLock? distributedLock =
            await distributedLockRepository.AcquireLockAsync(
                request.Id, cancellationToken);

        if (distributedLock?.IsAcquired is not true)
        {
            return Result.WithError(CommonApplicationErrors
                .DistributedLockErrors.NotAcquired);
        }

        Task<DriverPublicApiDto?> driverTask = driverPublicApi
            .GetByIdAsync(request.Body!.DriverId, cancellationToken);

        Domain.Aggregates.Ride? entity = await rideRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            return Result<Domain.Aggregates.Ride>.FromNotFound(
                DomainErrors.RideError.NotFound);
        }

        DriverPublicApiDto? driver = await driverTask;

        if (driver is null)
        {
            return Result<Domain.Aggregates.Ride>.FromConflict(
                DomainErrors.RideError.NotFound);
        }

        if (driver.Status != DriverStatus.Active)
        {
            return Result<Domain.Aggregates.Ride>.FromRuleViolation(
                DomainErrors.RideError.DriverInactive);
        }

        Result resultstatusTransition = rideStatusTransitionValidator
            .Validate(entity.Status, RideStatus.Accepted);

        if (resultstatusTransition.IsFailure)
        {
            return resultstatusTransition;
        }
        
        Result resultAction = Accept(entity,
            request.Body!.DriverId);

        if (resultAction.IsFailure)
        {
            return resultAction;
        }

        await rideRepository.UpdateAsync(
            entity, cancellationToken);

        await eventDispatcher.DispatchAsync(entity, cancellationToken);
        
        metricsPublisher.IncrementAccepted();

        logger.LogProcessed(
            ModuleConstants.ContextName,
            nameof(AcceptRideCommandHandler),
            nameof(Handle), entity.Id);

        return Result.WithSuccess();
    }

    private Result Accept(Domain.Aggregates.Ride entity, Guid driverId)
    {
        Result result = entity.Accept(driverId);

        if (result.IsFailure)
        {
            logger.LogGenericError(
                ModuleConstants.ContextName,
                nameof(AcceptRideCommandHandler),
                nameof(Accept), entity.Id, result.FirstError!);
        }

        return result;
    }
}
