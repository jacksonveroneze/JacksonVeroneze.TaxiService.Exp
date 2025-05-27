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

        Result<RideEntity> resultEntity = await GetRideByIdAsync(
            request.Id, cancellationToken);

        if (resultEntity.IsFailure)
        {
            return resultEntity;
        }

        Result resultAction = Start(resultEntity.Value!);

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
        RideEntity? entity = await rideRepository
            .GetByIdAsync(id, cancellationToken);

        if (entity is not null)
        {
            return Result<RideEntity>.WithSuccess(entity);
        }

        Error error = DomainErrors.RideError.NotFound;

        logger.LogNotFound(
            nameof(CancelRideCommandHandler),
            nameof(GetRideByIdAsync), id, error);

        return Result<RideEntity>.FromNotFound(error);
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

    private async Task UpdateAsync(
        RideEntity entity,
        CancellationToken cancellationToken)
    {
        await rideRepository.UpdateAsync(
            entity, cancellationToken);

        logger.LogProcessed(
            nameof(StartRideCommandHandler),
            nameof(Handle), entity.Id);
    }
}
