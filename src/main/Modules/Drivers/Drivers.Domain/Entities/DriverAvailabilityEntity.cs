using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseDomain.Entities.Base;
using TaxiService.Common.BaseDomain.Enums;
using TaxiService.Common.BaseDomain.ValueObjects;
using TaxiService.Modules.Drivers.Domain.Core.Errors;
using TaxiService.Modules.Drivers.Domain.Enums;

namespace TaxiService.Modules.Drivers.Domain.Entities;

public class DriverAvailabilityEntity : BaseEntityAggregateRoot
{
    public Guid Id { get; private set; }

    public Guid DriverId { get; private set; }

    public DriverAvailability? Availability { get; private set; }

    public CoordinateValueObject? Position { get; private set; }

    #region ctor

    public DriverAvailabilityEntity()
    {
    }

    public DriverAvailabilityEntity(Guid driverId,
        DriverAvailability? availability,
        CoordinateValueObject? position)
    {
        ArgumentNullException.ThrowIfNull(driverId);
        ArgumentNullException.ThrowIfNull(availability);
        ArgumentNullException.ThrowIfNull(position);

        DriverId = driverId;
        Availability = availability;
        Position = position;
    }

    #endregion

    #region Factory

    // Factory Method
    public static Result<DriverAvailabilityEntity> Create(
        Guid driverId,
        DriverAvailability? availability,
        float latitude, float longitude)
    {
        Result<CoordinateValueObject> coordinateVo =
            CoordinateValueObject.Create(latitude, longitude);

        if (coordinateVo.IsFailure)
        {
            return Result<DriverAvailabilityEntity>
                .FromInvalid(coordinateVo.Error!);
        }

        DriverAvailabilityEntity entity = new(driverId,
            availability, coordinateVo.Value);

        return Result<DriverAvailabilityEntity>.WithSuccess(entity);
    }

    #endregion
}