using JacksonVeroneze.TemplateWebApi.Domain.Entities.Base;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;

namespace JacksonVeroneze.TemplateWebApi.Domain.Entities;

public class AccountEntity : BaseEntity
{
    public string Number { get; private set; }

    public AccountStatus? Status { get; private set; }

    public BankEntity BankEntity { get; private set; }

    public List<ClientEntity>? Clients { get; private set; }

    public AccountEntity(string number, BankEntity bankEntity)
    {
        ArgumentException.ThrowIfNullOrEmpty(number);
        ArgumentNullException.ThrowIfNull(bankEntity);

        Number = number;
        BankEntity = bankEntity;

        Status = AccountStatus.PendingActivation;
    }
}
