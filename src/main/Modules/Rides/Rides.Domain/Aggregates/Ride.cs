using JacksonVeroneze.NET.Result;

using TaxiService.Common.BaseDomain;
using TaxiService.Common.BaseDomain.Entities.Base;
using TaxiService.Common.BaseDomain.ValueObjects;
using TaxiService.Modules.Rides.Domain.Core.Errors;
using TaxiService.Modules.Rides.Domain.Enums;
using TaxiService.Modules.Rides.Domain.Events;

namespace TaxiService.Modules.Rides.Domain.Aggregates;

public class Ride : BaseAggregateRoot
{
    public Guid Id { get; private set; }

    public Guid PassengerId { get; private set; }

    public Guid? DriverId { get; private set; }

    public decimal? Fare { get; private set; }

    public double? Distance { get; private set; }

    public CoordinateValueObject CoordinateFrom { get; private set; } = null!;

    public CoordinateValueObject CoordinateTo { get; private set; } = null!;

    public RideStatus Status { get; private set; }

    #region ctor

    protected Ride()
    {
    }

    private Ride(Guid passengerId,
        CoordinateValueObject coordinateFrom,
        CoordinateValueObject coordinateTo) : this()
    {
        ArgumentNullException.ThrowIfNull(coordinateFrom);
        ArgumentNullException.ThrowIfNull(coordinateTo);

        Id = GuidGenerator.Generate();
        PassengerId = passengerId;
        CoordinateFrom = coordinateFrom;
        CoordinateTo = coordinateTo;
        Status = RideStatus.Requested;
        
        AddEvent(new RideRequestedDomainEvent(Id));
    }

    #endregion

    #region Status

    public Result Accept(Guid? driverId)
    {
        if (IsAccepted)
        {
            return Result.FromInvalid(
                DomainErrors.RideError.StatusAlreadyDefined);
        }

        if (!IsRequested)
        {
            return Result.FromInvalid(
                DomainErrors.RideError.InvalidStatusSetAccept);
        }

        if (DriverId is not null)
        {
            return Result.FromInvalid(
                DomainErrors.RideError.DriverAlready);
        }

        DriverId = driverId;
        Status = RideStatus.Accepted;
        AddEvent(new RideAcceptedDomainEvent(Id));

        return Result.WithSuccess();
    }

    public Result Start()
    {
        if (InProgress)
        {
            return Result.FromInvalid(
                DomainErrors.RideError.StatusAlreadyDefined);
        }

        if (!IsAccepted)
        {
            return Result.FromInvalid(
                DomainErrors.RideError.InvalidStatusSetStart);
        }

        Status = RideStatus.InProgress;
        AddEvent(new RideStartedDomainEvent(Id));

        return Result.WithSuccess();
    }

    public Result Finish(double distance)
    {
        if (IsCompleted)
        {
            return Result.FromInvalid(
                DomainErrors.RideError.StatusAlreadyDefined);
        }

        if (!InProgress)
        {
            return Result.FromInvalid(
                DomainErrors.RideError.InvalidStatusSetFinish);
        }

        Distance = distance;
        Fare = Convert.ToDecimal(distance) * 2.1M;
        Status = RideStatus.Completed;
        AddEvent(new RideFinishedDomainEvent(Id));

        return Result.WithSuccess();
    }

    public Result Cancel()
    {
        if (IsCanceled)
        {
            return Result.FromInvalid(
                DomainErrors.RideError.StatusAlreadyDefined);
        }

        if (IsCompleted)
        {
            return Result.FromInvalid(
                DomainErrors.RideError.InvalidStatusSetCancel);
        }

        Status = RideStatus.Canceled;
        AddEvent(new RideCanceledDomainEvent(Id));

        return Result.WithSuccess();
    }

    public bool IsRequested => Status is RideStatus.Requested;
    public bool IsAccepted => Status is RideStatus.Accepted;
    public bool InProgress => Status is RideStatus.InProgress;
    public bool IsCompleted => Status is RideStatus.Completed;
    public bool IsCanceled => Status is RideStatus.Canceled;

    #endregion

    #region Factory

    public static Result<Ride> Create(Guid passengerId,
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
            return Result<Ride>
                .FromInvalid(resultValidate.Errors);
        }

        Ride entity = new(passengerId,
            coordinatefrom.Value!, coordinateTo.Value!);

        return Result<Ride>.WithSuccess(entity);
    }

    #endregion
}
