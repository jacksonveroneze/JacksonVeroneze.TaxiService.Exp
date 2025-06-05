using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseDomain;
using TaxiService.Common.BaseDomain.Entities.Base;
using TaxiService.Modules.Driver.Domain.Core.Errors;
using TaxiService.Modules.Driver.Domain.Enums;

namespace TaxiService.Modules.Driver.Domain.Entities;

public class DriverAvailabilityHistory : BaseEntity
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

    public DriverAvailabilityHistory()
    {
    }

    protected DriverAvailabilityHistory(Guid driverId,
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
            return Result.FromRuleViolation(
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
            return Result.FromRuleViolation(
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
            return Result.FromRuleViolation(
                DomainErrors.DriverAvailabilityError
                    .CannotStartRideWhileOffline);
        }

        Status = DriverAvailabilityStatus.InRide;

        return Result.WithSuccess();
    }

    #endregion

    #region Factory

    public static Result<DriverAvailabilityHistory> Create(
        Guid driverId,
        DriverAvailabilityStatus availability)
    {
        DriverAvailabilityHistory entity1 = new(
            driverId, availability);

        return Result<DriverAvailabilityHistory>.WithSuccess(entity1);
    }

    #endregion
}
