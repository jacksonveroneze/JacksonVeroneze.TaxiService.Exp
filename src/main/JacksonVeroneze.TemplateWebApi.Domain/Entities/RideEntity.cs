using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.DomainEvents.Ride;
using JacksonVeroneze.TemplateWebApi.Domain.Entities.Base;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;
using JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

namespace JacksonVeroneze.TemplateWebApi.Domain.Entities;

public class RideEntity : BaseEntityAggregateRoot
{
    private readonly IReadOnlyCollection<PositionEntity> _emptyPositions =
        Enumerable.Empty<PositionEntity>().ToList().AsReadOnly();

    public virtual UserEntity? User { get; }

    public Guid UserId { get; set; }

    public virtual UserEntity? Driver { get; private set; }

    public virtual decimal? Fare { get; private set; }

    public virtual double? Distance { get; private set; }

    public virtual CoordinateValueObject? From { get; }

    public virtual CoordinateValueObject? To { get; }

    public virtual RideStatus Status { get; private set; }

    private List<PositionEntity>? _positions;

    public virtual IReadOnlyCollection<PositionEntity> Positions =>
        _positions?.AsReadOnly() ?? _emptyPositions;

    protected RideEntity()
    {
    }

    public RideEntity(UserEntity? user,
        CoordinateValueObject? from,
        CoordinateValueObject? to)
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

    #region Position

    public Result AddPosition(PositionEntity position)
    {
        ArgumentNullException.ThrowIfNull(position);

        _positions ??= new List<PositionEntity>();

        if (!_positions.Contains(position))
        {
            _positions.Add(position);
        }

        return Result.WithSuccess();
    }

    private double? CalculateDistance()
    {
        PositionEntity? fist = _positions?.First();

        return _positions?
            .Skip(1)
            .Sum(item =>
            {
                double result = CalculateDistanceTwoPoints(
                    fist!, item);

                fist = item;

                return result;
            });
    }

    private double CalculateDistanceTwoPoints(
        PositionEntity start, PositionEntity end)
    {
        return 1D;
    }

    #endregion

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

    public Result Finish()
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

        Distance = CalculateDistance();

        //Fare = Distance.Value * 2M;

        // calcular distância
        // calcular

        Status = RideStatus.Completed;

        AddEvent(new RideFinishedDomainEvent(Id));

        return Result.WithSuccess();
    }

    public Result Cancel()
    {
        if (Status == RideStatus.Canceled)
        {
            return Result.FromInvalid(
                DomainErrors.Ride.InvalidStatusSetCancel);
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
