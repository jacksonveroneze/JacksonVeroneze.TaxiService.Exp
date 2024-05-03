using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities.Base;
using JacksonVeroneze.TaxiService.Exp.Domain.ValueObjects;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Entities;

public class PositionEntity : BaseEntityAggregateRoot
{
    public Guid Id { get; private set; }

    public Guid RideId { get; private set; }

    public CoordinateValueObject? Position { get; private set; }

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