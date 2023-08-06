using JacksonVeroneze.TemplateWebApi.Domain.Entities.Base;
using JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

namespace JacksonVeroneze.TemplateWebApi.Domain.Entities;

public class Client : BaseEntity
{
    private readonly IReadOnlyCollection<Account> _emptyAccounts =
        Enumerable.Empty<Account>().ToList().AsReadOnly();

    public PersonName Name { get; private set; }

    public Email Mail { get; private set; }

    private List<Account>? _accounts;

    public IReadOnlyCollection<Account> Accounts =>
        _accounts?.AsReadOnly() ?? _emptyAccounts;

    public Client(PersonName name, Email mail)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(mail);

        Name = name;
        Mail = mail;
    }

    public void AddAccount(Account account)
    {
        ArgumentNullException.ThrowIfNull(account);

        _accounts ??= new List<Account>();

        _accounts.Add(account);
    }

    public void UpdateAccount(Account account)
    {
        ArgumentNullException.ThrowIfNull(account);

        if (!(_accounts?.Contains(account) ?? false))
        {
            return;
        }

        _accounts.Remove(account);
        _accounts.Add(account);
    }

    public void RemoveAccount(Account account)
    {
        ArgumentNullException.ThrowIfNull(account);

        if (!(_accounts?.Contains(account) ?? false))
        {
            return;
        }

        _accounts.Remove(account);
    }
}
