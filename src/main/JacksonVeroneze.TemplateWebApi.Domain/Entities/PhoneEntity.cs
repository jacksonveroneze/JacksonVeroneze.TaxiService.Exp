using JacksonVeroneze.TemplateWebApi.Domain.Entities.Base;
using JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

namespace JacksonVeroneze.TemplateWebApi.Domain.Entities;

public class PhoneEntity : BaseEntity
{
    public PhoneValueObject Phone { get; private set; }

    public PhoneEntity(PhoneValueObject phone)
    {
        ArgumentNullException.ThrowIfNull(phone);

        Phone = phone;
    }
}
