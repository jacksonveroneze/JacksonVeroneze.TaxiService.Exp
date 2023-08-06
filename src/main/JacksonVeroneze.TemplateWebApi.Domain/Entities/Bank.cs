using JacksonVeroneze.TemplateWebApi.Domain.Entities.Base;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;

namespace JacksonVeroneze.TemplateWebApi.Domain.Entities;

public class Bank : BaseEntity
{
    public string? Name { get; private set; }

    public BankStatus? Status { get; private set; }

    public Bank(string? name)
    {
        Name = name;

        Status = BankStatus.PendingActivation;
    }

    public void Activate()
    {
        if (Status != BankStatus.PendingActivation)
        {
            throw new InvalidOperationException("Status não permitido");
        }

        Status = BankStatus.Active;
    }

    public void Inativate()
    {
        if (Status != BankStatus.Active)
        {
            throw new InvalidOperationException("Status não permitido");
        }

        Status = BankStatus.Inactive;
    }
}
