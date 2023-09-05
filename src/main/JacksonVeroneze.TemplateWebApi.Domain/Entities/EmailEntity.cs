using JacksonVeroneze.TemplateWebApi.Domain.Entities.Base;
using JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

namespace JacksonVeroneze.TemplateWebApi.Domain.Entities;

public class EmailEntity : BaseEntity
{
    public virtual EmailValueObject Email { get; private set; }

    public virtual UserEntity User { get; private set; }

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
