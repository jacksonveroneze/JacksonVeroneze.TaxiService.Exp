using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Primitives;
using JacksonVeroneze.TemplateWebApi.Domain.DomainEvents;
using JacksonVeroneze.TemplateWebApi.Domain.Entities.Base;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;

namespace JacksonVeroneze.TemplateWebApi.Domain.Entities;

public class BankEntity : BaseEntity
{
    public string Name { get; private set; }

    public BankStatus Status { get; set; }

    public DateTime? ActivedOnUtc { get; private set; }

    public DateTime? InactivedOnUtc { get; private set; }

    public BankEntity(string name)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);

        Name = name;

        Status = BankStatus.PendingActivation;
    }

    public void ChangeName(string name)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);

        Name = name;
    }

    public IResult Activate(DateTime utcNow)
    {
        if (Status != BankStatus.PendingActivation)
        {
            return Result.Invalid(
                DomainErrors.Bank.AlreadyProcessed);
        }

        Status = BankStatus.Active;

        ActivedOnUtc = utcNow;

        AddEvent(new BankActivatedEvent(Id));

        return Result.Success();
    }

    public IResult Inactivate(DateTime utcNow)
    {
        if (Status != BankStatus.PendingActivation)
        {
            return Result.Invalid(
                DomainErrors.Bank.AlreadyProcessed);
        }

        Status = BankStatus.Inactive;

        InactivedOnUtc = utcNow;

        AddEvent(new BankInactivatedEvent(Id));

        return Result.Success();
    }
}
