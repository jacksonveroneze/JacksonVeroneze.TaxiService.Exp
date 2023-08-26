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

    public NameValueObject Name { get; private set; }

    public DateTime Birthday { get; private set; }

    public Gender Gender { get; private set; }

    public UserStatus Status { get; private set; }

    public DateTime? ActivedOnUtc { get; private set; }

    public DateTime? InactivedOnUtc { get; private set; }

    private List<EmailEntity>? _emails;

    private List<PhoneEntity>? _phones;

    public IReadOnlyCollection<EmailEntity> Emails =>
        _emails?.AsReadOnly() ?? _emptyEmails;

    public IReadOnlyCollection<PhoneEntity> Phones =>
        _phones?.AsReadOnly() ?? _emptyPhones;

    public UserEntity(NameValueObject name, DateTime birthday, Gender gender)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(birthday);
        ArgumentNullException.ThrowIfNull(gender);

        Name = name;
        Birthday = birthday;
        Gender = gender;

        Status = UserStatus.PendingActivation;

        AddEvent(new UserCreatedDomainEvent(Id));
    }

    #region Active/Inative

    public IResult Activate(DateTime utcNow)
    {
        if (Status == UserStatus.Active)
        {
            return Result.Invalid(
                DomainErrors.User.AlreadyActivated);
        }

        Status = UserStatus.Active;

        ActivedOnUtc = utcNow;

        AddEvent(new UserActivatedDomainEvent(Id));

        return Result.Success();
    }

    public IResult Inactivate(DateTime utcNow)
    {
        if (Status == UserStatus.Inactive)
        {
            return Result.Invalid(
                DomainErrors.User.AlreadyInactivated);
        }

        Status = UserStatus.Inactive;

        InactivedOnUtc = utcNow;

        AddEvent(new UserInactivatedDomainEvent(Id));

        return Result.Success();
    }

    #endregion

    #region Email

    public IResult AddEmail(EmailEntity email)
    {
        ArgumentNullException.ThrowIfNull(email);

        _emails ??= new List<EmailEntity>();

        if (_emails.Contains(email))
        {
            return Result.Invalid(
                DomainErrors.User.DuplicateEmail);
        }

        _emails.Add(email);

        return Result.Success();
    }

    public IResult UpdateEmail(EmailEntity email)
    {
        ArgumentNullException.ThrowIfNull(email);

        if (!(_emails?.Contains(email) ?? false))
        {
            return Result.Invalid(
                DomainErrors.User.EmailNotFound);
        }

        _emails.Remove(email);
        _emails.Add(email);

        return Result.Success();
    }

    public IResult RemoveEmail(EmailEntity email)
    {
        ArgumentNullException.ThrowIfNull(email);

        if (!(_emails?.Contains(email) ?? false))
        {
            return Result.Invalid(
                DomainErrors.User.EmailNotFound);
        }

        _emails.Remove(email);

        return Result.Success();
    }

    public EmailEntity? GetEmailById(Guid id)
    {
        return Emails.FirstOrDefault(
            item => item.Id == id);
    }

    public EmailEntity? GetEmailByValue(EmailValueObject value)
    {
        return Emails.FirstOrDefault(
            item => item.Email == value);
    }

    #endregion

    #region Phone

    public IResult AddPhone(PhoneEntity phone)
    {
        ArgumentNullException.ThrowIfNull(phone);

        _phones ??= new List<PhoneEntity>();

        if (_phones.Contains(phone))
        {
            return Result.Invalid(
                DomainErrors.User.DuplicatePhone);
        }

        _phones.Add(phone);

        return Result.Success();
    }

    public IResult UpdatePhone(PhoneEntity phone)
    {
        ArgumentNullException.ThrowIfNull(phone);

        if (!(_phones?.Contains(phone) ?? false))
        {
            return Result.Invalid(
                DomainErrors.User.PhoneNotFound);
        }

        _phones.Remove(phone);
        _phones.Add(phone);

        return Result.Success();
    }

    public IResult RemovePhone(PhoneEntity phone)
    {
        ArgumentNullException.ThrowIfNull(phone);

        if (!(_phones?.Contains(phone) ?? false))
        {
            return Result.Invalid(
                DomainErrors.User.PhoneNotFound);
        }

        _phones.Remove(phone);

        return Result.Success();
    }

    public PhoneEntity? GetPhoneById(Guid id)
    {
        return Phones.FirstOrDefault(
            item => item.Id == id);
    }

    public PhoneEntity? GetPhoneByValue(PhoneValueObject value)
    {
        return Phones.FirstOrDefault(
            item => item.Phone == value);
    }

    #endregion
}
