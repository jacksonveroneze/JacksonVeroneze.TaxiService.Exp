using JacksonVeroneze.NET.MongoDB.DomainObjects;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;
using JacksonVeroneze.TemplateWebApi.Domain.Exceptions;

namespace JacksonVeroneze.TemplateWebApi.Domain.Entities;

public class BankEntity : BaseEntity<Guid>
{
    public string Name { get; set; }

    public BankStatus Status { get; set; }

    public BankEntity(string name)
    {
        Id = Guid.NewGuid();

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
