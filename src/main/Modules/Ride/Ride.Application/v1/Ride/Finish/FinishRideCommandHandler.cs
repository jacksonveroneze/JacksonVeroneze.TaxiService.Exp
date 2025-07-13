using JacksonVeroneze.NET.Result;
using Ride.Common;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseDomain.ValueObjects;
using TaxiService.Common.SharedKernel.Interfaces;
using TaxiService.Modules.Ride.Application.Common.Interfaces;
using TaxiService.Modules.Ride.Application.Common.Interfaces.Repositories;
using TaxiService.Modules.Ride.Domain.Core.Errors;
using TaxiService.Modules.Ride.Domain.Repositories;
using TaxiService.Modules.Ride.Domain.Services;

namespace TaxiService.Modules.Ride.Application.v1.Ride.Finish;

public sealed class FinishRideCommandHandler(
    ILogger<FinishRideCommandHandler> logger,
    IRideRepository rideRepository,
    IPositionReadRepository positionReadRepository,
    IDistanceCalculatorService distanceCalculatorService,
    IDomainEventPublisher eventPublisher,
    IMetricsPublisher metricsPublisher)
    : IRequestHandler<FinishRideCommand, Result>
{
    public async Task<Result> Handle(
        FinishRideCommand request,
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

        double distance = await CalculateDistanceAsync(
            request.Id, cancellationToken);

        Result resultAction = Finish(entity, distance);

        if (resultAction.IsFailure)
        {
            return resultAction;
        }

        await rideRepository.UpdateAsync(
            entity, cancellationToken);

        await eventPublisher.PublishAsync(entity, cancellationToken);

        metricsPublisher.IncrementFinished();
        
        logger.LogProcessed(
            ModuleConstants.ContextName,
            nameof(FinishRideCommandHandler),
            nameof(Handle), entity.Id);

        return Result.WithSuccess();
    }

    private Result Finish(Domain.Aggregates.Ride entity, double distance)
    {
        Result result = entity.Finish(distance);

        if (result.IsFailure)
        {
            logger.LogGenericError(
                ModuleConstants.ContextName,
                nameof(FinishRideCommandHandler),
                nameof(Finish), entity.Id, result.FirstError!);
        }

        return result;
    }

    private async Task<double> CalculateDistanceAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        ICollection<Domain.Entities.Position> positions = await positionReadRepository
            .GetByRideIdAsync(id, cancellationToken);

        CoordinateValueObject[] positionsCood = positions
            .Select(pos => CoordinateValueObject.Create(
                pos.Coordinate.Latitude, pos.Coordinate.Longitude).Value!)
            .ToArray();

        return distanceCalculatorService
            .Calculate(positionsCood);
    }
}
