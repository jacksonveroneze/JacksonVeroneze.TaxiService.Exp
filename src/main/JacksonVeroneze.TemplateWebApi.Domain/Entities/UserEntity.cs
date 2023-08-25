using JacksonVeroneze.NET.DomainObjects.Domain;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Primitives;
using JacksonVeroneze.TemplateWebApi.Domain.DomainEvents;
using JacksonVeroneze.TemplateWebApi.Domain.Entities.Base;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;
using JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

namespace JacksonVeroneze.TemplateWebApi.Domain.Entities;

public class UserEntity : BaseEntity, IAggregateRoot
{
    private readonly IReadOnlyCollection<EmailEntity> _emptyEmails =
        Enumerable.Empty<EmailEntity>().ToList().AsReadOnly();

    private readonly IReadOnlyCollection<PhoneEntity> _emptyPhones =
        Enumerable.Empty<PhoneEntity>().ToList().AsReadOnly();

    public PersonName Name { get; private set; }

    public DateTime Birthday { get; private set; }

    public UserStatus Status { get; private set; }

    public DateTime? ActivedOnUtc { get; private set; }

    public DateTime? InactivedOnUtc { get; private set; }

    private List<EmailEntity>? _emails;

    private List<PhoneEntity>? _phones;

    public IReadOnlyCollection<EmailEntity> Emails =>
        _emails?.AsReadOnly() ?? _emptyEmails;

    public IReadOnlyCollection<PhoneEntity> Phones =>
        _phones?.AsReadOnly() ?? _emptyPhones;

    public UserEntity(PersonName name, DateTime birthday)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(birthday);

        Name = name;
        Birthday = birthday;

        Status = UserStatus.PendingActivation;
    }

    #region Active/Inative

    public IResult Activate(DateTime utcNow)
    {
        if (Status != UserStatus.PendingActivation)
        {
            return Result.Invalid(
                DomainErrors.User.AlreadyProcessed);
        }

        Status = UserStatus.Active;

        ActivedOnUtc = utcNow;

        AddEvent(new UserActivatedEvent(Id));

        return Result.Success();
    }

    public IResult Inactivate(DateTime utcNow)
    {
        if (Status != UserStatus.Active)
        {
            return Result.Invalid(
                DomainErrors.User.AlreadyProcessed);
        }

        Status = UserStatus.Inactive;

        InactivedOnUtc = utcNow;

        AddEvent(new UserInactivatedEvent(Id));

        return Result.Success();
    }

    #endregion

    #region Email

    public void AddEmail(EmailEntity emailEntity)
    {
        ArgumentNullException.ThrowIfNull(emailEntity);

        _emails ??= new List<EmailEntity>();

        _emails.Add(emailEntity);
    }

    public void UpdateEmail(EmailEntity emailEntity)
    {
        ArgumentNullException.ThrowIfNull(emailEntity);

        if (!(_emails?.Contains(emailEntity) ?? false))
        {
            return;
        }

        _emails.Remove(emailEntity);
        _emails.Add(emailEntity);
    }

    public void RemoveEmail(EmailEntity emailEntity)
    {
        ArgumentNullException.ThrowIfNull(emailEntity);

        if (!(_emails?.Contains(emailEntity) ?? false))
        {
            return;
        }

        _emails.Remove(emailEntity);
    }

    #endregion

    #region Phone

    public void AddPhone(PhoneEntity phoneEntity)
    {
        ArgumentNullException.ThrowIfNull(phoneEntity);

        _phones ??= new List<PhoneEntity>();

        _phones.Add(phoneEntity);
    }

    public void UpdatePhone(PhoneEntity phoneEntity)
    {
        ArgumentNullException.ThrowIfNull(phoneEntity);

        if (!(_phones?.Contains(phoneEntity) ?? false))
        {
            return;
        }

        _phones.Remove(phoneEntity);
        _phones.Add(phoneEntity);
    }

    public void RemovePhone(PhoneEntity phoneEntity)
    {
        ArgumentNullException.ThrowIfNull(phoneEntity);

        if (!(_phones?.Contains(phoneEntity) ?? false))
        {
            return;
        }

        _phones.Remove(phoneEntity);
    }

    #endregion
}
