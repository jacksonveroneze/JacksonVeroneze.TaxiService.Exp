using JacksonVeroneze.TaxiService.Exp.Domain.Entities.Base;
using JacksonVeroneze.TaxiService.Exp.Domain.ValueObjects;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Entities;

public class EmailEntity : BaseEntityAggregateRoot
{
    public virtual EmailValueObject Email { get; } = null!;

    public virtual UserEntity User { get; } = null!;

    protected EmailEntity()
    {
    }

    public EmailEntity(UserEntity user, EmailValueObject email)
    {
        ArgumentNullException.ThrowIfNull(user);
        ArgumentNullException.ThrowIfNull(email);

        User = user;
        Email = email;
    }
}
