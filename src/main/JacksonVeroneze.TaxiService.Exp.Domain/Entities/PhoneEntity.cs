using JacksonVeroneze.TaxiService.Exp.Domain.Entities.Base;
using JacksonVeroneze.TaxiService.Exp.Domain.ValueObjects;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Entities;

public class PhoneEntity : BaseEntityAggregateRoot
{
    public virtual PhoneValueObject? Phone { get; private set; }

    public virtual UserEntity User { get; private set; } = null!;

    protected PhoneEntity()
    {
    }

    public PhoneEntity(UserEntity user, PhoneValueObject phone)
    {
        ArgumentNullException.ThrowIfNull(user);
        ArgumentNullException.ThrowIfNull(phone);

        User = user;
        Phone = phone;
    }
}
