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
    }

    #region Position

    public IResult AddPosition(PositionEntity position)
    {
        ArgumentNullException.ThrowIfNull(position);

        _positions ??= new List<PositionEntity>();

        if (!_positions.Contains(position))
        {
            _positions.Add(position);
        }

        return Result.Success();
    }

    #endregion

    #region Status

    public IResult Accept(UserEntity driver)
    {
        ArgumentNullException.ThrowIfNull(driver);

        if (Status != RideStatus.Requested)
        {
            return Result.Invalid(
                DomainErrors.Ride.InvalidStatusSetAccept);
        }

        if (Driver is not null)
        {
            return Result.Invalid(
                DomainErrors.Ride.DriverAlready);
        }

        Driver = driver;
        Status = RideStatus.Accepted;

        AddEvent(new RideAcceptedDomainEvent(Id));

        return Result.Success();
    }

    public IResult Start()
    {
        if (Status != RideStatus.Accepted)
        {
            return Result.Invalid(
                DomainErrors.Ride.InvalidStatusSetStart);
        }

        Status = RideStatus.InProgress;

        AddEvent(new RideStartedDomainEvent(Id));

        return Result.Success();
    }

    public IResult Finish()
    {
        if (Status != RideStatus.InProgress)
        {
            return Result.Invalid(
                DomainErrors.Ride.InvalidStatusSetFinish);
        }

        Status = RideStatus.Completed;

        AddEvent(new RideFinishedDomainEvent(Id));

        return Result.Success();
    }

    public IResult Cancel()
    {
        if (Status is RideStatus.Completed or RideStatus.Canceled)
        {
            return Result.Invalid(
                DomainErrors.Ride.InvalidStatusSetCancel);
        }

        Status = RideStatus.Canceled;

        AddEvent(new RideCanceledDomainEvent(Id));

        return Result.Success();
    }

    #endregion
}
