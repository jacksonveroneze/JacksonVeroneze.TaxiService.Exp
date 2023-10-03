using JacksonVeroneze.NET.DomainObjects.Domain;
using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.DomainEvents;
using JacksonVeroneze.TemplateWebApi.Domain.Entities.Base;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;
using JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

namespace JacksonVeroneze.TemplateWebApi.Domain.Entities;

public class UserEntity : BaseEntityAggregateRoot, IAggregateRoot
{
    private readonly IReadOnlyCollection<EmailEntity> _emptyEmails =
        Enumerable.Empty<EmailEntity>().ToList().AsReadOnly();

    public NameValueObject Name { get; private set; } = null!;

    public DateOnly Birthday { get; private set; }

    public Gender Gender { get; private set; }

    public CpfValueObject Cpf { get; private set; } = null!;

    public UserStatus Status { get; private set; }

    public DateTime? ActivedOnUtc { get; private set; }

    public DateTime? InactivedOnUtc { get; private set; }

    private List<EmailEntity>? _emails;

    public virtual IReadOnlyCollection<EmailEntity> Emails =>
        _emails?.AsReadOnly() ?? _emptyEmails;

    protected UserEntity()
    {
    }

    public UserEntity(NameValueObject name,
        DateOnly birthday, Gender gender,
        CpfValueObject cpf)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(birthday);
        ArgumentNullException.ThrowIfNull(gender);
        ArgumentNullException.ThrowIfNull(cpf);

        Name = name;
        Birthday = birthday;
        Gender = gender;
        Cpf = cpf;

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

    public IResult AddEmail(EmailEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        _emails ??= new List<EmailEntity>();

        if (ExistsEmailByValue(entity.Email))
        {
            return Result.Invalid(
                DomainErrors.Email.DuplicateEmail);
        }

        _emails.Add(entity);

        return Result.Success();
    }

    public IResult RemoveEmail(EmailEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        _emails ??= new List<EmailEntity>();

        if (!ExistsEmailByValue(entity.Email))
        {
            return Result.Invalid(
                DomainErrors.Email.NotFound);
        }

        _emails.Remove(entity);

        return Result.Success();
    }

    public EmailEntity? GetEmailById(Guid id)
    {
        return Emails.FirstOrDefault(
            item => item.Id == id);
    }

    public bool ExistsEmailByValue(EmailValueObject value)
    {
        return Emails.Any(
            entity => entity.Email.Value == value.Value);
    }

    #endregion

    public static UserEntity Create(NameValueObject name,
        DateOnly birthday, Gender gender,
        CpfValueObject cpf)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(birthday);
        ArgumentNullException.ThrowIfNull(gender);
        ArgumentNullException.ThrowIfNull(cpf);

        return new UserEntity(name, birthday, gender, cpf);
    }
}
