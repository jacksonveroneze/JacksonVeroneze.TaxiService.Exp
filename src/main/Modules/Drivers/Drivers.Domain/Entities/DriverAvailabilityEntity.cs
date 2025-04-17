using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseDomain;
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

    public DriverAvailabilityStatus? Status { get; private set; }

    public bool IsInRide =>
        Status is DriverAvailabilityStatus.InRide;

    public bool IsOnline =>
        Status is DriverAvailabilityStatus.Online;

    public bool IsOffline =>
        Status is DriverAvailabilityStatus.Offline;

    #region ctor

    public DriverAvailabilityEntity()
    {
    }

    protected DriverAvailabilityEntity(Guid driverId,
        DriverAvailabilityStatus status)
    {
        Id = GuidGenerator.Generate();
        DriverId = driverId;
        Status = status;
    }

    #endregion

    #region Status

    public Result SetOnline()
    {
        if (IsInRide)
        {
            return Result.FromInvalid(
                DomainErrors.DriverAvailabilityError
                    .CannotGoOnlineDuringRide);
        }

        Status = DriverAvailabilityStatus.Online;

        return Result.WithSuccess();
    }

    public Result SetOffline()
    {
        if (IsInRide)
        {
            return Result.FromInvalid(
                DomainErrors.DriverAvailabilityError
                    .CannotGoOfflineDuringRide);
        }

        Status = DriverAvailabilityStatus.Offline;

        return Result.WithSuccess();
    }

    public Result SetInRide()
    {
        if (IsOffline)
        {
            return Result.FromInvalid(
                DomainErrors.DriverAvailabilityError
                    .CannotStartRideWhileOffline);
        }

        Status = DriverAvailabilityStatus.InRide;

        return Result.WithSuccess();
    }

    #endregion

    #region Factory

    // Factory Method
    public static Result<DriverAvailabilityEntity> Create(
        Guid driverId,
        DriverAvailabilityStatus availability)
    {
        DriverAvailabilityEntity entity1 = new(
            driverId, availability);

        return Result<DriverAvailabilityEntity>.WithSuccess(entity1);
    }

    #endregion
}