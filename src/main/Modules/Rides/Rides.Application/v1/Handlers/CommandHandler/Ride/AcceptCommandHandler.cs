using System.Diagnostics;
using JacksonVeroneze.NET.Result;
using RedLockNet;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Drivers.PublicApi.Interfaces;
using TaxiService.Modules.Rides.Application.v1.Commands.Ride;
using TaxiService.Modules.Rides.Application.v1.Interfaces.Repositories.Ride;
using TaxiService.Modules.Rides.Domain.Core.Errors;
using TaxiService.Modules.Rides.Domain.Entities;

namespace TaxiService.Modules.Rides.Application.v1.Handlers.CommandHandler.Ride;

public sealed class AcceptCommandHandler(
    ILogger<AcceptCommandHandler> logger,
    IDriverService driverService,
    IRideReadRepository rideReadRepository,
    IRideWriteRepository rideWriteRepository,
    IRideDistributedLockRepository distributedLockRepository)
    : IRequestHandler<AcceptCommand, Result>
{
    private static readonly ActivitySource ActivitySource =
        new("TaxiService");

    public async Task<Result> Handle(
        AcceptCommand request,
        CancellationToken cancellationToken)
    {
        using Activity? activity = ActivitySource.StartActivity();

        ArgumentNullException.ThrowIfNull(request);

        await using IRedLock? distributedLock =
            await distributedLockRepository.AcquireLockAsync(
                request.Id, cancellationToken);

        if (distributedLock?.IsAcquired is not true)
        {
            return Result.WithError(
                new Error("Lock", "Lock não adquirido"));
        }

        Task<Result<bool>> resultDriverTask = ExistsDriverByIdAsync(
            request.Body!.DriverId, cancellationToken);

        Result<RideEntity> resultEntity = await GetRideByIdAsync(
            request.Id, cancellationToken);

        if (resultEntity.IsFailure)
        {
            return resultEntity;
        }

        Result<bool> resultDriver = await resultDriverTask;

        if (resultDriver.IsFailure)
        {
            return resultDriver;
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
        using Activity? activity = ActivitySource.StartActivity();

        RideEntity? entity = await rideReadRepository
            .GetByIdAsync(id, cancellationToken);

        if (entity is not null)
        {
            return Result<RideEntity>.WithSuccess(entity);
        }

        Error error = DomainErrors.RideError.NotFound;

        logger.LogNotFound(
            nameof(CancelCommandHandler),
            nameof(GetRideByIdAsync), id, error);

        return Result<RideEntity>.FromNotFound(error);
    }

    private async Task<Result<bool>> ExistsDriverByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        using Activity? activity = ActivitySource.StartActivity();

        Result<bool> entity = await driverService
            .ExistsByIdAsync(id, cancellationToken);

        return entity;
    }

    private Result Accept(RideEntity entity, Guid driverId)
    {
        using Activity? activity = ActivitySource.StartActivity();

        Result result = entity.Accept(driverId);

        if (result.IsFailure)
        {
            logger.LogGenericError(
                nameof(AcceptCommandHandler),
                nameof(Accept), entity.Id, result.Error!);
        }

        return result;
    }

    private async Task UpdateAsync(
        RideEntity entity,
        CancellationToken cancellationToken)
    {
        using Activity? activity = ActivitySource.StartActivity();

        await rideWriteRepository.UpdateAsync(
            entity, cancellationToken);

        logger.LogProcessed(
            nameof(AcceptCommandHandler),
            nameof(UpdateAsync), entity.Id);
    }
}