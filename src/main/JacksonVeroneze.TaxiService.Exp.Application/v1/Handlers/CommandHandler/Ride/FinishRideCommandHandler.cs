using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.Extensions;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.Ride;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Position;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Ride;
using JacksonVeroneze.TaxiService.Exp.Domain.Core.Errors;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Services;
using JacksonVeroneze.TaxiService.Exp.Domain.ValueObjects;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Handlers.CommandHandler.Ride;

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

        Result<RideEntity> resultRide = await GetRideByIdAsync(
            request.Id, cancellationToken);

        if (resultRide.IsFailure)
        {
            return resultRide;
        }

        double distance = await CalculateDistanceAsync(
            request.Id, cancellationToken);

        Result resultAction = Finish(resultRide.Value!, distance);

        if (resultAction.IsFailure)
        {
            return resultAction;
        }

        await UpdateAsync(resultRide.Value!, cancellationToken);

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

    private Result Finish(RideEntity ride, double distance)
    {
        Result result = ride.Finish(distance);

        if (result.IsSuccess)
        {
            return Result.WithSuccess();
        }

        logger.LogGenericError(nameof(FinishRideCommandHandler),
            nameof(Handle), ride.Id, result.Error!);

        return result;
    }

    private async Task UpdateAsync(
        RideEntity ride,
        CancellationToken cancellationToken)
    {
        await rideWriteRepository.UpdateAsync(
            ride, cancellationToken);

        logger.LogProcessed(nameof(FinishRideCommandHandler),
            nameof(Handle), ride.Id);
    }
}