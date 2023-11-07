using JacksonVeroneze.TemplateWebApi.Domain.Entities.Base;
using JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

namespace JacksonVeroneze.TemplateWebApi.Domain.Entities;

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
