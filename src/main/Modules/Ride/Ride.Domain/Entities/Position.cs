using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseDomain;
using TaxiService.Common.BaseDomain.Entities.Base;
using TaxiService.Common.BaseDomain.ValueObjects;

namespace TaxiService.Modules.Ride.Domain.Entities;

public class Position : BaseEntity
{
    public Guid Id { get; private set; }

    public Guid RideId { get; private set; }

    public CoordinateValueObject Coordinate { get; private set; } = null!;

    #region ctor

    protected Position()
    {
    }

    private Position(
        Guid rideId,
        CoordinateValueObject coordinate)
    {
        Id = GuidGenerator.Generate();
        RideId = rideId;
        Coordinate = coordinate;
    }

    #endregion

    #region Factory

    public static Result<Position> Create(
        Guid rideId, float latitude, float longitude)
    {
        Result<CoordinateValueObject> coordinateVo =
            CoordinateValueObject.Create(latitude, longitude);

        if (coordinateVo.IsFailure)
        {
            return Result<Position>
                .FromInvalid(coordinateVo.FirstError!);
        }

        Position entity = new(rideId, coordinateVo.Value!);

        return Result<Position>.WithSuccess(entity);
    }

    #endregion
}
