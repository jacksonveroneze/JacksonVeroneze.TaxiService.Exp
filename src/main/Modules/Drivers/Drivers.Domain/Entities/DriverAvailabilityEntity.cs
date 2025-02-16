using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseDomain.Entities.Base;
using TaxiService.Common.BaseDomain.Enums;
using TaxiService.Common.BaseDomain.ValueObjects;
using TaxiService.Modules.Drivers.Domain.Core.Errors;
using TaxiService.Modules.Drivers.Domain.Enums;

namespace TaxiService.Modules.Drivers.Domain.Entities;

public class DriverAvailabilityEntity : BaseEntityAggregateRoot
{
    public Guid Id { get; private set; }

    public Guid DriverId { get; private set; }

    public DriverAvailability? Status { get; private set; }

    public CoordinateValueObject Location { get; private set; } = null!;

    #region ctor

    public DriverAvailabilityEntity()
    {
    }

    public DriverAvailabilityEntity(Guid driverId,
        DriverAvailability? status,
        CoordinateValueObject? location)
    {
        ArgumentNullException.ThrowIfNull(driverId);
        ArgumentNullException.ThrowIfNull(status);
        ArgumentNullException.ThrowIfNull(location);

        DriverId = driverId;
        Status = DriverAvailability.Offline;
    }

    #endregion

    #region Status

    public Result SetOnline(double latitude, double longitude)
    {
        if (Status is DriverAvailability.InRide)
        {
            return Result.FromInvalid(
                DomainErrors.DriverAvailabilityError
                    .CannotGoOnlineDuringRide);
        }
        
        Result<CoordinateValueObject> coordinateVo =
            CoordinateValueObject.Create(latitude, longitude);

        if (coordinateVo.IsFailure)
        {
            return Result<DriverAvailabilityEntity>
                .FromInvalid(coordinateVo.Error!);
        }
        
        Status = DriverAvailability.Online;
        Location = coordinateVo.Value!;
        MarkAsUpdated();

        return Result.WithSuccess();
    }
    
    public Result SetOffline()
    {
        if (Status is DriverAvailability.InRide)
        {
            return Result.FromInvalid(
                DomainErrors.DriverAvailabilityError
                    .CannotGoOfflineDuringRide);
        }

        Status = DriverAvailability.Offline;
        MarkAsUpdated();

        return Result.WithSuccess();
    }
    
    public Result SetInRide()
    {
        if (Status is DriverAvailability.Offline)
        {
            return Result.FromInvalid(
                DomainErrors.DriverAvailabilityError
                    .CannotStartRideWhileOffline);
        }

        Status = DriverAvailability.InRide;
        MarkAsUpdated();

        return Result.WithSuccess();
    }

    public Result UpdateLocation(
        double latitude, double longitude)
    {
        Result<CoordinateValueObject> coordinateVo =
            CoordinateValueObject.Create(latitude, longitude);

        if (coordinateVo.IsFailure)
        {
            return Result<DriverAvailabilityEntity>
                .FromInvalid(coordinateVo.Error!);
        }

        Location = coordinateVo.Value!;
        MarkAsUpdated();

        return Result.WithSuccess();
    }

    #endregion

    #region Factory

    // Factory Method
    public static Result<DriverAvailabilityEntity> Create(
        Guid driverId,
        DriverAvailability? availability,
        float latitude, float longitude)
    {
        Result<CoordinateValueObject> coordinateVo =
            CoordinateValueObject.Create(latitude, longitude);

        if (coordinateVo.IsFailure)
        {
            return Result<DriverAvailabilityEntity>
                .FromInvalid(coordinateVo.Error!);
        }

        DriverAvailabilityEntity entity = new(driverId,
            availability, coordinateVo.Value);

        return Result<DriverAvailabilityEntity>.WithSuccess(entity);
    }

    #endregion
}