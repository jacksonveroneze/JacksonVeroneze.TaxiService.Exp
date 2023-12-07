using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.Extensions;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.Ride;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Position;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Ride;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Base;
using JacksonVeroneze.TaxiService.Exp.Domain.Core.Errors;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Services;
using JacksonVeroneze.TaxiService.Exp.Domain.ValueObjects;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Handlers.CommandHandler.Ride;

public sealed class FinishRideCommandHandler(
    ILogger<FinishRideCommandHandler> logger,
    IRideReadRepository readRepository,
    IRideWriteRepository writeRepository,
    IDistanceCalculatorService distanceCalculatorService,
    IPositionReadRepository positionReadRepository)
    : IRequestHandler<FinishRideCommand, Result<VoidResponse>>
{
    public async Task<Result<VoidResponse>> Handle(
        FinishRideCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        RideEntity? ride = await readRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (ride is null)
        {
            return Result<VoidResponse>
                .FromNotFound(DomainErrors.Ride.NotFound);
        }

        List<PositionEntity> positions = await positionReadRepository
            .GetByRideIdAsync(request.Id, cancellationToken);

        CoordinateValueObject[] positionsCood = positions
            .Select(pos => CoordinateValueObject.Create(
                pos.Position!.Latitude, pos.Position!.Longitude).Value!)
            .ToArray();

        double distance = distanceCalculatorService
            .Calculate(positionsCood);

        Result result = ride.Finish(distance);

        if (result.IsFailure)
        {
            logger.LogGenericError(nameof(FinishRideCommandHandler),
                nameof(Handle), ride.Id, result.Error!);

            return Result<VoidResponse>.WithError(result.Error!);
        }

        await writeRepository.UpdateAsync(ride, cancellationToken);

        logger.LogProcessed(nameof(FinishRideCommandHandler),
            nameof(Handle), ride.Id);

        return Result<VoidResponse>.WithSuccess();
    }
}
