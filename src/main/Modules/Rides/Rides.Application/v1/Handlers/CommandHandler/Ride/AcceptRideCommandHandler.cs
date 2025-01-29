using System.Diagnostics;
using JacksonVeroneze.NET.Result;
using RedLockNet;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Passengers.PublicApi.Interfaces;
using TaxiService.Modules.Rides.Application.v1.Commands.Ride;
using TaxiService.Modules.Rides.Application.v1.Interfaces.Repositories.Ride;
using TaxiService.Modules.Rides.Domain.Core.Errors;
using TaxiService.Modules.Rides.Domain.Entities;

namespace TaxiService.Modules.Rides.Application.v1.Handlers.CommandHandler.Ride;

public sealed class AcceptRideCommandHandler(
    ILogger<AcceptRideCommandHandler> logger,
    IUserService userService,
    IRideReadRepository rideReadRepository,
    IRideWriteRepository rideWriteRepository,
    IDistributedLockFactory distributedLockFactory)
    : IRequestHandler<AcceptRideCommand, Result>
{
    private const string PrefixLockKey = "_lock_ride_action_accept_";

    // Lock expira após 5 segundos
    private readonly TimeSpan _expiryTime = TimeSpan.FromSeconds(10);

    // Tempo máximo de espera para adquirir o lock
    private readonly TimeSpan _waitTime = TimeSpan.FromSeconds(5);

    // Intervalo de 2 segundos entre tentativas
    private readonly TimeSpan _retryDelay = TimeSpan.FromSeconds(1);

    private static readonly ActivitySource ActivitySource =
        new("TaxiService");

    public async Task<Result> Handle(
        AcceptRideCommand request,
        CancellationToken cancellationToken)
    {
        using Activity? activity = ActivitySource.StartActivity();

        ArgumentNullException.ThrowIfNull(request);

        await using IRedLock? distributedLock =
            await AcquireLockAsync(request.Id);

        if (distributedLock is null)
        {
            return Result.WithError(
                new Error("Lock", "Lock não adquirido"));
        }

        Task<Result<bool>> resultDriverTask = GetDriverByIdAsync(
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

    private async Task<IRedLock?> AcquireLockAsync(Guid rideId)
    {
        using Activity? activity = ActivitySource.StartActivity();

        string lockKey = $"{PrefixLockKey}{rideId}";

        IRedLock? distributedLock = await distributedLockFactory
            .CreateLockAsync(lockKey, _expiryTime, _waitTime, _retryDelay);

        return distributedLock.IsAcquired ? distributedLock : null;
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
            nameof(CancelRideCommandHandler),
            nameof(GetRideByIdAsync), id, error);

        return Result<RideEntity>.FromNotFound(error);
    }

    private async Task<Result<bool>> GetDriverByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        using Activity? activity = ActivitySource.StartActivity();

        Result<bool> entity = await userService
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
                nameof(AcceptRideCommandHandler),
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
            nameof(AcceptRideCommandHandler),
            nameof(UpdateAsync), entity.Id);
    }
}