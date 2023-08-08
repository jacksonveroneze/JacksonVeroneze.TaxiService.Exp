using JacksonVeroneze.TemplateWebApi.Domain.Entities.Base;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;

namespace JacksonVeroneze.TemplateWebApi.Domain.Entities;

public class AccountEntity : BaseEntity
{
    private readonly IReadOnlyCollection<KeyEntity> _emptyKeys =
        Enumerable.Empty<KeyEntity>().ToList().AsReadOnly();

    public string Number { get; private set; }

    public AccountStatus? Status { get; private set; }

    public BankEntity BankEntity { get; private set; }

    public List<ClientEntity>? Clients { get; private set; }

    private List<KeyEntity>? _keys;

    public IReadOnlyCollection<KeyEntity> Keys =>
        _keys?.AsReadOnly<KeyEntity>() ?? _emptyKeys;

    public AccountEntity(string number, BankEntity bankEntity)
    {
        ArgumentException.ThrowIfNullOrEmpty(number);
        ArgumentNullException.ThrowIfNull(bankEntity);

        Number = number;
        BankEntity = bankEntity;

        Status = AccountStatus.PendingActivation;
    }

    public void AddKey(KeyEntity keyEntity)
    {
        ArgumentNullException.ThrowIfNull(keyEntity);

        _keys ??= new List<KeyEntity>();

        _keys.Add(keyEntity);
    }

    public void UpdateKey(KeyEntity keyEntity)
    {
        ArgumentNullException.ThrowIfNull(keyEntity);

        if (!(_keys?.Contains(keyEntity) ?? false))
        {
            return;
        }

        _keys.Remove(keyEntity);
        _keys.Add(keyEntity);
    }

    public void RemoveKey(KeyEntity keyEntity)
    {
        ArgumentNullException.ThrowIfNull(keyEntity);

        if (!(_keys?.Contains(keyEntity) ?? false))
        {
            return;
        }

        _keys.Remove(keyEntity);
    }
}
