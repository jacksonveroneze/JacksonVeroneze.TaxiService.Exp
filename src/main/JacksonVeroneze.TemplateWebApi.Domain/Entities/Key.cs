using JacksonVeroneze.TemplateWebApi.Domain.Entities.Base;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;

namespace JacksonVeroneze.TemplateWebApi.Domain.Entities;

public class Key : BaseEntity
{
    public KeyType? Type { get; private set; }

    public string? Value { get; private set; }

    public KeyStatus? Status { get; private set; }

    public Account? Account { get; private set; }

    public Key(KeyType? type, string? value, Account? account)
    {
        Type = type;
        Value = value;
        Account = account;

        Status = KeyStatus.PendingActivation;
    }

    public void Activate()
    {
        if (Status != KeyStatus.PendingActivation)
        {
            throw new InvalidOperationException("Status não permitido");
        }

        Status = KeyStatus.Active;
    }

    public void Inativate()
    {
        if (Status != KeyStatus.Active)
        {
            throw new InvalidOperationException("Status não permitido");
        }

        Status = KeyStatus.Inactive;
    }

    public override bool IsValid()
        =>  Type != KeyType.None && Status != KeyStatus.None;
    }
