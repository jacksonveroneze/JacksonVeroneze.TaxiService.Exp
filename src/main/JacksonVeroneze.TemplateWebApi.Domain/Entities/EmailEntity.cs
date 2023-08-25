using JacksonVeroneze.TemplateWebApi.Domain.Entities.Base;
using JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

namespace JacksonVeroneze.TemplateWebApi.Domain.Entities;

public class EmailEntity : BaseEntity
{
    public Email Email { get; private set; }

    public EmailEntity(Email email)
    {
        ArgumentNullException.ThrowIfNull(email);

        Email = email;
    }
}
