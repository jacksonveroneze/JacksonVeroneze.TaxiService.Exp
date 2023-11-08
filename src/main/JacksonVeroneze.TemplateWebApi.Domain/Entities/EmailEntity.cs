using JacksonVeroneze.TemplateWebApi.Domain.Entities.Base;
using JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

namespace JacksonVeroneze.TemplateWebApi.Domain.Entities;

public class EmailEntity : BaseEntityAggregateRoot
{
    public virtual EmailValueObject Email { get; }

    public virtual UserEntity User { get; }

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
