using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Passengers.Application.Extensions;
using TaxiService.Modules.Passengers.Application.v1.Commands.Ride;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Position;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Ride;
using TaxiService.Modules.Passengers.Domain.Entities;
using TaxiService.Modules.Passengers.Domain.Services;
using TaxiService.Modules.Passengers.Domain.ValueObjects;
using TaxiService.Modules.Rides.Application.Core.Errors;

namespace TaxiService.Modules.Passengers.Application.v1.Handlers.CommandHandler.Ride;

public sealed class FinishRideCommandHandler(
    ILogger<FinishRideCommandHandler> logger,
    IRideReadRepository rideReadRepository,
    IRideWriteRepository rideWriteRepository,
    IPositionReadRepository positionReadRepository,
    IDistanceCalculatorService distanceCalculatorService)
    : IRequestHandler<FinishRideCommand, Result>
{
    public async Task<Result> Handle(
        FinishRideCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result<RideEntity> resultEntity = await GetRideByIdAsync(
            request.Id, cancellationToken);

        if (resultEntity.IsFailure)
        {
            return resultEntity;
        }

        double distance = await CalculateDistanceAsync(
            request.Id, cancellationToken);

        Result resultAction = Finish(resultEntity.Value!, distance);

        if (resultAction.IsFailure)
        {
            return resultAction;
        }

        await UpdateAsync(resultEntity.Value!, cancellationToken);

        return Result.WithSuccess();
    }

    private async Task<Result<RideEntity>> GetRideByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        RideEntity? ride = await rideReadRepository
            .GetByIdAsync(id, cancellationToken);

        if (ride is null)
        {
            return Result<RideEntity>.FromNotFound(
                DomainErrors.RideError.NotFound);
        }

        return Result<RideEntity>.WithSuccess(ride);
    }

    private async Task<double> CalculateDistanceAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        ICollection<PositionEntity> positions = await positionReadRepository
            .GetByRideIdAsync(id, cancellationToken);

        CoordinateValueObject[] positionsCood = positions
            .Select(pos => CoordinateValueObject.Create(
                pos.Position!.Latitude, pos.Position!.Longitude).Value!)
            .ToArray();

        return distanceCalculatorService
            .Calculate(positionsCood);
    }

    private Result Finish(RideEntity entity, double distance)
    {
        Result result = entity.Finish(distance);

        if (result.IsFailure)
        {
            logger.LogGenericError(
                nameof(FinishRideCommandHandler),
                nameof(Finish), entity.Id, result.Error!);
        }

        return result;
    }

    private async Task UpdateAsync(
        RideEntity entity,
        CancellationToken cancellationToken)
    {
        await rideWriteRepository.UpdateAsync(
            entity, cancellationToken);

        logger.LogProcessed(
            nameof(FinishRideCommandHandler),
            nameof(UpdateAsync), entity.Id);
    }
}