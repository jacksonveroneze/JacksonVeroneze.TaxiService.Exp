using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseDomain.Entities.Base;
using TaxiService.Common.BaseDomain.ValueObjects;
using TaxiService.Modules.Rides.Domain.Core.Errors;
using TaxiService.Modules.Rides.Domain.Enums;

namespace TaxiService.Modules.Rides.Domain.Entities;

public class RideEntity : BaseEntityAggregateRoot
{
    public Guid Id { get; private set; }

    public Guid? UserId { get; private set; }

    public Guid? DriverId { get; private set; }

    public decimal? Fare { get; private set; }

    public double? Distance { get; private set; }

    public CoordinateValueObject CoordinateFrom { get; private set; } = null!;

    public CoordinateValueObject CoordinateTo { get; private set; } = null!;

    public RideStatus Status { get; private set; }

    #region ctor

    protected RideEntity()
    {
    }

    private RideEntity(Guid userId,
        CoordinateValueObject coordinateFrom,
        CoordinateValueObject coordinateTo)
    {
        ArgumentNullException.ThrowIfNull(coordinateFrom);
        ArgumentNullException.ThrowIfNull(coordinateTo);

        Id = Guid.NewGuid();
        UserId = userId;
        CoordinateFrom = coordinateFrom;
        CoordinateTo = coordinateTo;

        Status = RideStatus.Requested;
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

        return Result.WithSuccess();
    }

    public bool IsRequested => Status is RideStatus.Requested;
    public bool IsAccepted => Status is RideStatus.Accepted;
    public bool InProgress => Status is RideStatus.InProgress;
    public bool IsCompleted => Status is RideStatus.Completed;
    public bool IsCanceled => Status is RideStatus.Canceled;

    #endregion

    #region Factory

    public static Result<RideEntity> Create(Guid userId,
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

        RideEntity entity = new(userId,
            coordinatefrom.Value!, coordinateTo.Value!);

        return Result<RideEntity>.WithSuccess(entity);
    }

    #endregion
}