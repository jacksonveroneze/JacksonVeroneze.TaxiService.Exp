using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseDomain.ValueObjects;
using TaxiService.Common.SharedKernel;
using TaxiService.Common.SharedKernel.Utils;
using TaxiService.Modules.Ride.Contracts.Events;
using TaxiService.Modules.Ride.Contracts.Events.DomainEvents;
using TaxiService.Modules.Ride.Domain.Core.Errors;
using TaxiService.Modules.Ride.Domain.Enums;

namespace TaxiService.Modules.Ride.Domain.Aggregates;

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

        AddEvent(new RideRequestedDomainEvent(Id, passengerId,
            coordinateFrom.Latitude, coordinateFrom.Longitude,
            coordinateTo.Latitude, coordinateTo.Longitude,
            DateTime.UtcNow));
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
        AddEvent(new RideAcceptedDomainEvent(
            Id, PassengerId, DriverId!.Value, DateTime.UtcNow));

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
        AddEvent(new RideStartedDomainEvent(
            Id, DriverId!.Value, PassengerId, DateTime.UtcNow));

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
        AddEvent(new RideFinishedDomainEvent(
            Id, PassengerId, DriverId!.Value,
            DateTime.UtcNow, TimeSpan.Zero,
            Distance!.Value, Fare!.Value));

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
        AddEvent(new RideCanceledDomainEvent(
            Id, DriverId, PassengerId, DateTime.UtcNow));

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
