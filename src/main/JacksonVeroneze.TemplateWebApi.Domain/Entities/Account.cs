using JacksonVeroneze.TemplateWebApi.Domain.Entities.Base;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;

namespace JacksonVeroneze.TemplateWebApi.Domain.Entities;

public class Account : BaseEntity
{
    private readonly IReadOnlyCollection<Key> _emptyKeys =
        Enumerable.Empty<Key>().ToList().AsReadOnly();

    public string Number { get; private set; }

    public AccountStatus? Status { get; private set; }

    public Bank Bank { get; private set; }

    public List<Client>? Clients { get; private set; }

    private List<Key>? _keys;

    public IReadOnlyCollection<Key> Keys =>
        _keys?.AsReadOnly<Key>() ?? _emptyKeys;

    public Account(string number, Bank bank)
    {
        ArgumentException.ThrowIfNullOrEmpty(number);
        ArgumentNullException.ThrowIfNull(bank);

        Number = number;
        Bank = bank;

        Status = AccountStatus.PendingActivation;
    }

    public void AddKey(Key key)
    {
        ArgumentNullException.ThrowIfNull(key);

        _keys ??= new List<Key>();

        _keys.Add(key);
    }

    public void UpdateKey(Key key)
    {
        ArgumentNullException.ThrowIfNull(key);

        if (!(_keys?.Contains(key) ?? false))
        {
            return;
        }

        _keys.Remove(key);
        _keys.Add(key);
    }

    public void RemoveKey(Key key)
    {
        ArgumentNullException.ThrowIfNull(key);

        if (!(_keys?.Contains(key) ?? false))
        {
            return;
        }

        _keys.Remove(key);
    }
}
