using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Domain.Core.Errors;
using JacksonVeroneze.TaxiService.Exp.Domain.DomainEvents.Ride;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities.Base;
using JacksonVeroneze.TaxiService.Exp.Domain.Enums;
using JacksonVeroneze.TaxiService.Exp.Domain.ValueObjects;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Entities;

public class RideEntity : BaseEntityAggregateRoot
{
    public virtual UserEntity? User { get; }

    public Guid UserId { get; set; }

    public virtual UserEntity? Driver { get; private set; }

    public virtual decimal? Fare { get; private set; }

    public virtual double? Distance { get; private set; }

    public virtual CoordinateValueObject? From { get; }

    public virtual CoordinateValueObject? To { get; }

    public virtual RideStatus Status { get; private set; }

    protected RideEntity()
    {
    }

    private RideEntity(UserEntity user,
        CoordinateValueObject from,
        CoordinateValueObject to)
    {
        ArgumentNullException.ThrowIfNull(user);
        ArgumentNullException.ThrowIfNull(from);
        ArgumentNullException.ThrowIfNull(to);

        User = user;
        From = from;
        To = to;

        Status = RideStatus.Requested;

        AddEvent(new RideRequestedDomainEvent(Id));
    }

    public static Result<RideEntity> Create(UserEntity user,
        float latitudeFrom, float longitudeFrom,
        float latitudeTo, float longitudeTo)
    {
        Result<CoordinateValueObject> coordinatefrom =
            CoordinateValueObject.Create(latitudeFrom, longitudeFrom);

        Result<CoordinateValueObject> coordinateTo =
            CoordinateValueObject.Create(latitudeTo, longitudeTo);

        Result resultValidate = Result
            .FailuresOrSuccess(coordinatefrom, coordinateTo);

        if (resultValidate.IsFailure)
        {
            return Result<RideEntity>
                .FromInvalid(resultValidate.Errors!);
        }

        RideEntity entity = new(user,
            coordinatefrom.Value!, coordinateTo.Value!);

        return Result<RideEntity>.WithSuccess(entity);
    }

    #region Status

    public Result Accept(UserEntity driver)
    {
        ArgumentNullException.ThrowIfNull(driver);

        if (Status == RideStatus.Accepted)
        {
            return Result.FromInvalid(
                DomainErrors.Ride.StatusAlreadyDefined);
        }

        if (Status != RideStatus.Requested)
        {
            return Result.FromInvalid(
                DomainErrors.Ride.InvalidStatusSetAccept);
        }

        if (Driver is not null)
        {
            return Result.FromInvalid(
                DomainErrors.Ride.DriverAlready);
        }

        Driver = driver;
        Status = RideStatus.Accepted;

        AddEvent(new RideAcceptedDomainEvent(Id));

        return Result.WithSuccess();
    }

    public Result Start()
    {
        if (Status == RideStatus.InProgress)
        {
            return Result.FromInvalid(
                DomainErrors.Ride.StatusAlreadyDefined);
        }

        if (Status != RideStatus.Accepted)
        {
            return Result.FromInvalid(
                DomainErrors.Ride.InvalidStatusSetStart);
        }

        Status = RideStatus.InProgress;

        AddEvent(new RideStartedDomainEvent(Id));

        return Result.WithSuccess();
    }

    public Result Finish(double distance)
    {
        if (Status == RideStatus.Completed)
        {
            return Result.FromInvalid(
                DomainErrors.Ride.StatusAlreadyDefined);
        }

        if (Status != RideStatus.InProgress)
        {
            return Result.FromInvalid(
                DomainErrors.Ride.InvalidStatusSetFinish);
        }

        Distance = distance;
        Fare = Convert.ToDecimal(distance) * 2.1M;

        Status = RideStatus.Completed;

        AddEvent(new RideFinishedDomainEvent(Id));

        return Result.WithSuccess();
    }

    public Result Cancel()
    {
        if (Status == RideStatus.Canceled)
        {
            return Result.FromInvalid(
                DomainErrors.Ride.StatusAlreadyDefined);
        }

        if (Status == RideStatus.Completed)
        {
            return Result.FromInvalid(
                DomainErrors.Ride.InvalidStatusSetCancel);
        }

        Status = RideStatus.Canceled;

        AddEvent(new RideCanceledDomainEvent(Id));

        return Result.WithSuccess();
    }

    #endregion
}
