using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.Messaging;
using TaxiService.Modules.Rides.Domain.Core.Errors;
using TaxiService.Modules.Rides.Domain.Repositories;

namespace TaxiService.Modules.Rides.Application.v1.Ride.Start;

public sealed class StartRideCommandHandler(
    ILogger<StartRideCommandHandler> logger,
    IRideRepository rideRepository,
    IDomainEventDispatcher eventDispatcher)
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

        Result resultAction = Start(entity);

        if (resultAction.IsFailure)
        {
            return resultAction;
        }

        await rideRepository.UpdateAsync(
            entity, cancellationToken);

        await eventDispatcher.DispatchAsync(entity);

        logger.LogProcessed(
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
                nameof(StartRideCommandHandler),
                nameof(Start), entity.Id, result.FirstError!);
        }

        return result;
    }
}
