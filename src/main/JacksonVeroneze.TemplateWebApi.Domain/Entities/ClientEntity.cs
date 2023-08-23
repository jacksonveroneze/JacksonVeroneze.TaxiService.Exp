using JacksonVeroneze.NET.DomainObjects.Domain;
using JacksonVeroneze.TemplateWebApi.Domain.Entities.Base;
using JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

namespace JacksonVeroneze.TemplateWebApi.Domain.Entities;

public class ClientEntity : BaseEntity, IAggregateRoot
{
    private readonly IReadOnlyCollection<AccountEntity> _emptyAccounts =
        Enumerable.Empty<AccountEntity>().ToList().AsReadOnly();

    public PersonName Name { get; private set; }

    public Email Email { get; private set; }

    private List<AccountEntity>? _accounts;

    public IReadOnlyCollection<AccountEntity> Accounts =>
        _accounts?.AsReadOnly() ?? _emptyAccounts;

    public ClientEntity(PersonName name, Email email)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(email);

        Name = name;
        Email = email;
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
