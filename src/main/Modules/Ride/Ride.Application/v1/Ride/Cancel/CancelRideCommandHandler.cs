using JacksonVeroneze.NET.Result;
using Ride.Common;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.SharedKernel.Interfaces;
using TaxiService.Common.SharedKernel.Utils;
using TaxiService.Modules.Ride.Application.Common.Interfaces;
using TaxiService.Modules.Ride.Domain.Core.Errors;
using TaxiService.Modules.Ride.Domain.Repositories;

namespace TaxiService.Modules.Ride.Application.v1.Ride.Cancel;

public sealed class CancelRideCommandHandler(
    ILogger<CancelRideCommandHandler> logger,
    IRideRepository rideRepository,
    IDomainEventPublisher eventPublisher,
    IMetricsPublisher metricsPublisher)
    : IRequestHandler<CancelRideCommand, Result>
{
    public async Task<Result> Handle(
        CancelRideCommand request,
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

        Result resultAction = Cancel(entity);

        if (resultAction.IsFailure)
        {
            return resultAction;
        }

        await rideRepository.UpdateAsync(
            entity, cancellationToken);

        await eventPublisher.PublishAsync(entity, cancellationToken);

        metricsPublisher.Counter("ride_canceled_total");

        logger.LogProcessed(
            ModuleConstants.ContextName,
            nameof(CancelRideCommandHandler),
            nameof(Handle), entity.Id);

        return Result.WithSuccess();
    }

    private Result Cancel(Domain.Aggregates.Ride entity)
    {
        Result result = entity.Cancel();

        if (result.IsFailure)
        {
            logger.LogGenericError(
                ModuleConstants.ContextName,
                nameof(CancelRideCommandHandler),
                nameof(Cancel), entity.Id, result.FirstError!);
        }

        return result;
    }
}
