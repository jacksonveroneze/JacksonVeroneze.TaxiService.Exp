using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.Ride;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Position;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Services.Ride;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Base;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Services;
using JacksonVeroneze.TaxiService.Exp.Domain.ValueObjects;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Handlers.CommandHandler.Ride;

public sealed class FinishRideCommandHandler(
    IGetRideService rideService,
    IStatusRideService statusRideService,
    IDistanceCalculatorService distanceCalculatorService,
    IPositionReadRepository positionReadRepository)
    : IRequestHandler<FinishRideCommand, Result<VoidResponse>>
{
    public async Task<Result<VoidResponse>> Handle(
        FinishRideCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result<RideEntity> rideResult = await rideService
            .TryGetRideAsync(request.Id, cancellationToken);

        if (rideResult.IsFailure)
        {
            return Result<VoidResponse>
                .FromNotFound(rideResult.Error!);
        }

        List<PositionEntity> positions = await positionReadRepository
            .GetByRideIdAsync(request.Id, cancellationToken);

        CoordinateValueObject[] positionsCood = positions
            .Select(pos => CoordinateValueObject.Create(
                pos.Position!.Latitude, pos.Position!.Longitude).Value!)
            .ToArray();

        double distance = distanceCalculatorService
            .Calculate(positionsCood);

        Result result = await statusRideService
            .TryFinishAsync(rideResult.Value!,
                distance, cancellationToken);

        return result.IsSuccess
            ? Result<VoidResponse>.WithSuccess()
            : Result<VoidResponse>.WithError(result.Error!);
    }
}
