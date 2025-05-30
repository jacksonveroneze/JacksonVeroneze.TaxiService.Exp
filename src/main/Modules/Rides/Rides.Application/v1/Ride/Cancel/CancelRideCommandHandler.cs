using JacksonVeroneze.NET.Result;

using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Rides.Application.Common.Interfaces.Repositories.Ride;
using TaxiService.Modules.Rides.Domain.Core.Errors;
using TaxiService.Modules.Rides.Domain.Entities;

namespace TaxiService.Modules.Rides.Application.v1.Ride.Cancel;

public sealed class CancelRideCommandHandler(
    ILogger<CancelRideCommandHandler> logger,
    IRideRepository rideRepository)
    : IRequestHandler<CancelRideCommand, Result>
{
    public async Task<Result> Handle(
        CancelRideCommand request,
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

        Result resultAction = Cancel(entity);

        if (resultAction.IsFailure)
        {
            return resultAction;
        }

        await rideRepository.UpdateAsync(
            entity, cancellationToken);

        logger.LogProcessed(
            nameof(CancelRideCommandHandler),
            nameof(Handle), entity.Id);

        return Result.WithSuccess();
    }

    private Result Cancel(RideEntity entity)
    {
        Result result = entity.Cancel();

        if (result.IsFailure)
        {
            logger.LogGenericError(
                nameof(CancelRideCommandHandler),
                nameof(Cancel), entity.Id, result.FirstError!);
        }

        return result;
    }
}
