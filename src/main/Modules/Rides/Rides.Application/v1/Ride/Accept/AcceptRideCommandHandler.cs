using JacksonVeroneze.NET.Result;

using RedLockNet;

using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Drivers.PublicApi.Interfaces;
using TaxiService.Modules.Rides.Application.Common.Interfaces.Repositories.Ride;
using TaxiService.Modules.Rides.Application.v1.Ride.Cancel;
using TaxiService.Modules.Rides.Domain.Core.Errors;
using TaxiService.Modules.Rides.Domain.Entities;

namespace TaxiService.Modules.Rides.Application.v1.Ride.Accept;

public sealed class AcceptRideCommandHandler(
    ILogger<AcceptRideCommandHandler> logger,
    IDriverPublicApi driverPublicApi,
    IRideRepository rideRepository,
    IRideDistributedLockRepository distributedLockRepository)
    : IRequestHandler<AcceptRideCommand, Result>
{
    public async Task<Result> Handle(
        AcceptRideCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        await using IRedLock? distributedLock =
            await distributedLockRepository.AcquireLockAsync(
                request.Id, cancellationToken);

        if (distributedLock?.IsAcquired is not true)
        {
            return Result.WithError(
                Error.Create("Lock", "Lock não adquirido"));
        }

        Task<bool> existsDriverTask = ExistsDriverByIdAsync(
            request.Body!.DriverId, cancellationToken);

        Result<RideEntity> resultEntity = await GetRideByIdAsync(
            request.Id, cancellationToken);

        if (resultEntity.IsFailure)
        {
            return resultEntity;
        }

        bool existsDriver = await existsDriverTask;

        if (!existsDriver)
        {
            return Result.FromConflict(Error.Create("Driver", "Driver não encontrado"));
        }

        Result resultAction = Accept(resultEntity.Value!,
            request.Body!.DriverId);

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

    private async Task<bool> ExistsDriverByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        bool exists = await driverPublicApi
            .ExistsByIdAsync(id, cancellationToken);

        return exists;
    }

    private Result Accept(RideEntity entity, Guid driverId)
    {
        Result result = entity.Accept(driverId);

        if (result.IsFailure)
        {
            logger.LogGenericError(
                nameof(AcceptRideCommandHandler),
                nameof(Accept), entity.Id, result.FirstError!);
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
            nameof(AcceptRideCommandHandler),
            nameof(UpdateAsync), entity.Id);
    }
}
