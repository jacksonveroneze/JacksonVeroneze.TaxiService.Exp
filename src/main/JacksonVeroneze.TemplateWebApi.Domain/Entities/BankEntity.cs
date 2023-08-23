using JacksonVeroneze.NET.DomainObjects.Exception;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.Entities.Base;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;

namespace JacksonVeroneze.TemplateWebApi.Domain.Entities;

public class BankEntity : BaseEntity
{
    public string Name { get; set; }

    public BankStatus Status { get; set; }

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

    public void Inactivate()
    {
        if (Status != BankStatus.Active)
        {
            throw new DomainException(
                DomainErrors.Bank.StatusNotAllowed);
        }

        Status = BankStatus.Inactive;
    }
}
