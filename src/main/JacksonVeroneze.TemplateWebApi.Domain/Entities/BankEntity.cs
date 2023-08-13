using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.Entities.Base;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;
using JacksonVeroneze.TemplateWebApi.Domain.Exceptions;

namespace JacksonVeroneze.TemplateWebApi.Domain.Entities;

public class BankEntity : BaseEntity
{
    public string Name { get; private set; }

    public BankStatus? Status { get; private set; }

    public BankEntity(string name)
    {
        Name = name;

        Status = BankStatus.PendingActivation;
    }

    public void Activate()
    {
        if (Status != BankStatus.PendingActivation)
        {
            throw new DomainException(
                DomainErrors.Bank.StatusNotAllowed);
        }

        Status = BankStatus.Active;
    }

    public void Inativate()
    {
        if (Status != BankStatus.Active)
        {
            throw new DomainException(
                DomainErrors.Bank.StatusNotAllowed);
        }

        Status = BankStatus.Inactive;
    }
}
