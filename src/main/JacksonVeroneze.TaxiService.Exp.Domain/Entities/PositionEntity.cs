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

    public PositionEntity(RideEntity? ride,
        CoordinateValueObject? position)
    {
        ArgumentNullException.ThrowIfNull(ride);
        ArgumentNullException.ThrowIfNull(position);

        Ride = ride;
        Position = position;
    }
}
