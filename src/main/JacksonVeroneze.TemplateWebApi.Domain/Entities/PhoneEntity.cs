using JacksonVeroneze.TemplateWebApi.Domain.Entities.Base;

namespace JacksonVeroneze.TemplateWebApi.Domain.Entities;

public class PhoneEntity : BaseEntity
{
    public string Number { get; private set; }

    public PhoneEntity(string number)
    {
        ArgumentException.ThrowIfNullOrEmpty(number);

        Number = number;
    }
}
