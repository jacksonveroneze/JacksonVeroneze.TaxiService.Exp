using JacksonVeroneze.NET.Result;

using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Rides.Application.Common.Interfaces.Repositories.Ride;
using TaxiService.Modules.Rides.Application.v1.Ride.Cancel;
using TaxiService.Modules.Rides.Domain.Core.Errors;
using TaxiService.Modules.Rides.Domain.Entities;

namespace TaxiService.Modules.Rides.Application.v1.Ride.Start;

public sealed class StartRideCommandHandler(
    ILogger<StartRideCommandHandler> logger,
    IRideRepository rideRepository)
    : IRequestHandler<StartRideCommand, Result>
{
    public async Task<Result> Handle(
        StartRideCommand request,
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

        Result resultAction = Start(entity);

        if (resultAction.IsFailure)
        {
            return resultAction;
        }

        await rideRepository.UpdateAsync(
            entity, cancellationToken);

        logger.LogProcessed(
            nameof(StartRideCommandHandler),
            nameof(Handle), entity.Id);

        return Result.WithSuccess();
    }

    private Result Start(RideEntity entity)
    {
        Result result = entity.Start();

        if (result.IsFailure)
        {
            logger.LogGenericError(
                nameof(StartRideCommandHandler),
                nameof(Start), entity.Id, result.FirstError!);
        }

        return result;
    }
}
