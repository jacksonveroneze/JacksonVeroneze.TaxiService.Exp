using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities.Base;
using JacksonVeroneze.TaxiService.Exp.Domain.ValueObjects;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Entities;

public class PositionEntity : BaseEntityAggregateRoot
{
    public virtual RideEntity? Ride { get; }

    public virtual CoordinateValueObject? Position { get; }

    protected PositionEntity()
    {
    }

    private PositionEntity(RideEntity? ride,
        CoordinateValueObject? position)
    {
        ArgumentNullException.ThrowIfNull(ride);
        ArgumentNullException.ThrowIfNull(position);

        Ride = ride;
        Position = position;
    }

    public static Result<PositionEntity> Create(RideEntity ride,
        float latitude, float longitude)
    {
        Result<CoordinateValueObject> coordinateVo = CoordinateValueObject.Create(
            latitude, longitude);

        if (coordinateVo.IsFailure)
        {
            return Result<PositionEntity>
                .FromInvalid(coordinateVo.Error!);
        }

        PositionEntity entity = new(ride, coordinateVo.Value!);

        return Result<PositionEntity>.WithSuccess(entity);
    }
}