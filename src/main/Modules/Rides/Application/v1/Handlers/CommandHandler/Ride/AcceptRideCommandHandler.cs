using JacksonVeroneze.NET.Result;
using RedLockNet;
using TaxiService.Common.Application.Extensions;
using TaxiService.Modules.Passengers.PublicApi;
using TaxiService.Modules.Passengers.PublicApi.Interfaces;
using TaxiService.Modules.Passengers.PublicApi.Services;
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

    public async Task<Result> Handle(
        AcceptRideCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        await using IRedLock? distributedLock =
            await AcquireLockAsync(request.Id);

        if (distributedLock is null)
        {
            return Result.WithError(
                new Error("Lock", "Lock não adquirido"));
        }

        Result<RideEntity> resultEntity = await GetRideByIdAsync(
            request.Id, cancellationToken);

        if (resultEntity.IsFailure)
        {
            return resultEntity;
        }

        Result<UserResponse> resultDriver = await GetDriverByIdAsync(
            request.Body!.DriverId, cancellationToken);

        if (resultDriver.IsFailure)
        {
            return resultDriver;
        }

        Result resultAction = Accept(resultEntity.Value!,
            resultDriver.Value!);

        if (resultAction.IsFailure)
        {
            return resultAction;
        }

        await UpdateAsync(resultEntity.Value!, cancellationToken);

        return Result.WithSuccess();
    }

    private async Task<IRedLock?> AcquireLockAsync(Guid rideId)
    {
        string lockKey = $"{PrefixLockKey}{rideId}";

        IRedLock? distributedLock = await distributedLockFactory
            .CreateLockAsync(lockKey, _expiryTime, _waitTime, _retryDelay);

        return distributedLock.IsAcquired ? distributedLock : null;
    }

    private async Task<Result<RideEntity>> GetRideByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
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

    private async Task<Result<UserEntity>> GetDriverByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        UserEntity? entity = await userService
            .GetByIdAsync(id, cancellationToken);

        if (entity is not null)
        {
            return Result<UserEntity>.WithSuccess(entity);
        }

        Error error = DomainErrors.UserError.NotFound;

        logger.LogNotFound(
            nameof(AcceptRideCommandHandler),
            nameof(GetDriverByIdAsync), id, error);

        return Result<UserEntity>.FromNotFound(error);
    }

    private Result Accept(RideEntity entity, UserEntity driver)
    {
        Result result = entity.Accept(driver.Id);

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
        await rideWriteRepository.UpdateAsync(
            entity, cancellationToken);

        logger.LogProcessed(
            nameof(AcceptRideCommandHandler),
            nameof(UpdateAsync), entity.Id);
    }
}