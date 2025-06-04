using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseDomain;
using TaxiService.Common.BaseDomain.Entities.Base;
using TaxiService.Common.BaseDomain.ValueObjects;

namespace TaxiService.Modules.Drivers.Domain.Entities;

public class DriverLocationHistory : BaseEntity
{
    public Guid Id { get; private set; }

    public Guid DriverId { get; private set; }

    public CoordinateValueObject Location { get; private set; } = null!;

    #region ctor

    protected DriverLocationHistory()
    {
    }

    public DriverLocationHistory(Guid driverId,
        CoordinateValueObject location)
    {
        ArgumentNullException.ThrowIfNull(location);

        Id = GuidGenerator.Generate();
        DriverId = driverId;
        Location = location;
    }

    #endregion

    #region Factory

    public static Result<DriverLocationHistory> Create(
        Guid driverId,
        float latitude, float longitude)
    {
        Result<CoordinateValueObject> coordinateVo =
            CoordinateValueObject.Create(latitude, longitude);

        if (coordinateVo.IsFailure)
        {
            return Result<DriverLocationHistory>
                .FromInvalid(coordinateVo.FirstError!);
        }

        DriverLocationHistory entity = new(
            driverId, coordinateVo.Value!);

        return Result<DriverLocationHistory>.WithSuccess(entity);
    }

    #endregion
}
