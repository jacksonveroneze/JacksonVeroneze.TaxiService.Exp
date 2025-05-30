using JacksonVeroneze.NET.Result;

using RedLockNet;

using TaxiService.Common.BaseApplication.Core.Errors;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Drivers.Domain.Enums;
using TaxiService.Modules.Drivers.PublicApi.v1.Interfaces;
using TaxiService.Modules.Drivers.PublicApi.v1.Models;
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
            return Result.WithError(CommonApplicationErrors
                .DistributedLockErrors.NotAcquired);
        }

        Task<DriverPublicApiDto?> driverTask = driverPublicApi
            .GetByIdAsync(request.Body!.DriverId, cancellationToken);

        RideEntity? entity = await rideRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            return Result<RideEntity>.FromNotFound(
                DomainErrors.RideError.NotFound);
        }

        DriverPublicApiDto? driver = await driverTask;

        if (driver is null)
        {
            return Result<RideEntity>.FromConflict(
                DomainErrors.RideError.NotFound);
        }
        
        if (driver.Status != DriverStatus.Active)
        {
            return Result<RideEntity>.FromRuleViolation(
                DomainErrors.RideError.DriverNotActive);
        }

        Result resultAction = Accept(entity,
            request.Body!.DriverId);

        if (resultAction.IsFailure)
        {
            return resultAction;
        }

        await rideRepository.UpdateAsync(
            entity, cancellationToken);

        logger.LogProcessed(
            nameof(AcceptRideCommandHandler),
            nameof(Handle), entity.Id);

        return Result.WithSuccess();
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
}
