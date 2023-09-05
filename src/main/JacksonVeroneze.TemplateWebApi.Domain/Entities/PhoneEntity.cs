using JacksonVeroneze.TemplateWebApi.Domain.Entities.Base;
using JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

namespace JacksonVeroneze.TemplateWebApi.Domain.Entities;

public class PhoneEntity : BaseEntity
{
    public virtual PhoneValueObject? Phone { get; private set; }

    protected PhoneEntity()
    {
    }

    public PhoneEntity(PhoneValueObject phone)
    {
        ArgumentNullException.ThrowIfNull(phone);

        Phone = phone;
    }
}
