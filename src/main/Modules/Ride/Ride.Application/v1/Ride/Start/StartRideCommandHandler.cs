using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.Messaging;
using TaxiService.Modules.Ride.Application.Common.Interfaces;
using TaxiService.Modules.Ride.Domain;
using TaxiService.Modules.Ride.Domain.Core.Errors;
using TaxiService.Modules.Ride.Domain.Enums;
using TaxiService.Modules.Ride.Domain.Repositories;
using TaxiService.Modules.Ride.Domain.Services;

namespace TaxiService.Modules.Ride.Application.v1.Ride.Start;

public sealed class StartRideCommandHandler(
    ILogger<StartRideCommandHandler> logger,
    IRideRepository rideRepository,
    IRideStatusTransitionValidator rideStatusTransitionValidator,
    IDomainEventDispatcher eventDispatcher,
    IMetricsPublisher metricsPublisher)
    : IRequestHandler<StartRideCommand, Result>
{
    public async Task<Result> Handle(
        StartRideCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Domain.Aggregates.Ride? entity = await rideRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            return Result.FromNotFound(
                DomainErrors.RideError.NotFound);
        }

        Result resultstatusTransition = rideStatusTransitionValidator
            .Validate(entity.Status, RideStatus.InProgress);

        if (resultstatusTransition.IsFailure)
        {
            return resultstatusTransition;
        }
        
        Result resultAction = Start(entity);

        if (resultAction.IsFailure)
        {
            return resultAction;
        }

        await rideRepository.UpdateAsync(
            entity, cancellationToken);

        await eventDispatcher.DispatchAsync(entity, cancellationToken);

        metricsPublisher.IncrementStarted();
        
        logger.LogProcessed(
            ModuleConstants.ContextName,
            nameof(StartRideCommandHandler),
            nameof(Handle), entity.Id);

        return Result.WithSuccess();
    }

    private Result Start(Domain.Aggregates.Ride entity)
    {
        Result result = entity.Start();

        if (result.IsFailure)
        {
            logger.LogGenericError(
                ModuleConstants.ContextName,
                nameof(StartRideCommandHandler),
                nameof(Start), entity.Id, result.FirstError!);
        }

        return result;
    }
}
