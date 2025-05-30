using JacksonVeroneze.NET.Result;

using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseDomain.ValueObjects;
using TaxiService.Modules.Rides.Application.Common.Interfaces.Repositories.Position;
using TaxiService.Modules.Rides.Application.Common.Interfaces.Repositories.Ride;
using TaxiService.Modules.Rides.Domain.Core.Errors;
using TaxiService.Modules.Rides.Domain.Entities;
using TaxiService.Modules.Rides.Domain.Services;

namespace TaxiService.Modules.Rides.Application.v1.Ride.Finish;

public sealed class FinishRideCommandHandler(
    ILogger<FinishRideCommandHandler> logger,
    IRideRepository rideRepository,
    IPositionReadRepository positionReadRepository,
    IDistanceCalculatorService distanceCalculatorService)
    : IRequestHandler<FinishRideCommand, Result>
{
    public async Task<Result> Handle(
        FinishRideCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        RideEntity? entity = await rideRepository
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

        logger.LogProcessed(
            nameof(FinishRideCommandHandler),
            nameof(Handle), entity.Id);

        return Result.WithSuccess();
    }

    private Result Finish(RideEntity entity, double distance)
    {
        Result result = entity.Finish(distance);

        if (result.IsFailure)
        {
            logger.LogGenericError(
                nameof(FinishRideCommandHandler),
                nameof(Finish), entity.Id, result.FirstError!);
        }

        return result;
    }

    private async Task<double> CalculateDistanceAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        ICollection<PositionEntity> positions = await positionReadRepository
            .GetByRideIdAsync(id, cancellationToken);

        CoordinateValueObject[] positionsCood = positions
            .Select(pos => CoordinateValueObject.Create(
                pos.Position.Latitude, pos.Position.Longitude).Value!)
            .ToArray();

        return distanceCalculatorService
            .Calculate(positionsCood);
    }
}
