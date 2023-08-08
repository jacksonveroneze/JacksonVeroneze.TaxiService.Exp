using JacksonVeroneze.TemplateWebApi.Domain.Entities.Base;
using JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

namespace JacksonVeroneze.TemplateWebApi.Domain.Entities;

public class ClientEntity : BaseEntity
{
    private readonly IReadOnlyCollection<AccountEntity> _emptyAccounts =
        Enumerable.Empty<AccountEntity>().ToList().AsReadOnly();

    public PersonName Name { get; private set; }

    public Email Mail { get; private set; }

    private List<AccountEntity>? _accounts;

    public IReadOnlyCollection<AccountEntity> Accounts =>
        _accounts?.AsReadOnly() ?? _emptyAccounts;

    public ClientEntity(PersonName name, Email mail)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(mail);

        Name = name;
        Mail = mail;
    }

    public void AddAccount(AccountEntity accountEntity)
    {
        ArgumentNullException.ThrowIfNull(accountEntity);

        _accounts ??= new List<AccountEntity>();

        _accounts.Add(accountEntity);
    }

    public void UpdateAccount(AccountEntity accountEntity)
    {
        ArgumentNullException.ThrowIfNull(accountEntity);

        if (!(_accounts?.Contains(accountEntity) ?? false))
        {
            return;
        }

        _accounts.Remove(accountEntity);
        _accounts.Add(accountEntity);
    }

    public void RemoveAccount(AccountEntity accountEntity)
    {
        ArgumentNullException.ThrowIfNull(accountEntity);

        if (!(_accounts?.Contains(accountEntity) ?? false))
        {
            return;
        }

        _accounts.Remove(accountEntity);
    }
}
