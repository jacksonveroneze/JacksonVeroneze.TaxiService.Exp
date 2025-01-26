using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseDomain.Entities.Base;
using TaxiService.Common.BaseDomain.ValueObjects;

namespace TaxiService.Modules.Rides.Domain.Entities;

public class PositionEntity : BaseEntityAggregateRoot
{
    public Guid Id { get; private set; }

    public Guid RideId { get; private set; }

    public CoordinateValueObject Position { get; private set; } = null!;

    #region ctor

    protected PositionEntity()
    {
    }

    private PositionEntity(
        Guid rideId,
        CoordinateValueObject position)
    {
        Id = Guid.NewGuid();
        RideId = rideId;
        Position = position;
    }

    #endregion

    #region Factory

    public static Result<PositionEntity> Create(
        Guid rideId,
        float latitude, float longitude)
    {
        Result<CoordinateValueObject> coordinateVo =
            CoordinateValueObject.Create(latitude, longitude);

        if (coordinateVo.IsFailure)
        {
            return Result<PositionEntity>
                .FromInvalid(coordinateVo.Error!);
        }

        PositionEntity entity = new(rideId, coordinateVo.Value!);

        return Result<PositionEntity>.WithSuccess(entity);
    }

    #endregion
}