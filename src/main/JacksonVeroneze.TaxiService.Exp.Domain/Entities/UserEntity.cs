using JacksonVeroneze.NET.DomainObjects.Domain;
using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Domain.Core.Errors;
using JacksonVeroneze.TaxiService.Exp.Domain.DomainEvents.User;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities.Base;
using JacksonVeroneze.TaxiService.Exp.Domain.Enums;
using JacksonVeroneze.TaxiService.Exp.Domain.ValueObjects;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Entities;

public class UserEntity : BaseEntityAggregateRoot, IAggregateRoot
{
    private readonly IReadOnlyCollection<EmailEntity> _emptyEmails =
        Enumerable.Empty<EmailEntity>().ToList().AsReadOnly();

    public NameValueObject Name { get; private set; } = null!;

    public DateOnly Birthday { get; private set; }

    public GenderType GenderType { get; private set; }

    public CpfValueObject Cpf { get; private set; } = null!;

    public UserStatus Status { get; private set; }

    public DateTime? ActivedOnUtc { get; private set; }

    public DateTime? InactivedOnUtc { get; private set; }

    private List<EmailEntity>? _emails;

    public virtual IReadOnlyCollection<EmailEntity> Emails =>
        _emails?.AsReadOnly() ?? _emptyEmails;

    public UserEntity()
    {
    }

    private UserEntity(NameValueObject name,
        DateOnly birthday, GenderType genderType,
        CpfValueObject cpf)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(birthday);
        ArgumentNullException.ThrowIfNull(genderType);
        ArgumentNullException.ThrowIfNull(cpf);

        Name = name;
        Birthday = birthday;
        GenderType = genderType;
        Cpf = cpf;

        Status = UserStatus.PendingActivation;

        AddEvent(new UserCreatedDomainEvent(Id));
    }

    public static Result<UserEntity> Create(string? name,
        DateOnly birthday, GenderType genderType, string? cpf)
    {
        Result<NameValueObject> nameVo = NameValueObject.Create(name);
        Result<CpfValueObject> cpfVo = CpfValueObject.Create(cpf);

        Result resultValidate = Result.FailuresOrSuccess(nameVo, cpfVo);

        if (resultValidate.IsFailure)
        {
            return Result<UserEntity>
                .FromInvalid(resultValidate.Errors!);
        }

        UserEntity entity = new(nameVo.Value!, birthday,
            genderType, cpfVo.Value!);

        return Result<UserEntity>.WithSuccess(entity);
    }

    #region Active/Inative

    public Result Activate(DateTime utcNow)
    {
        if (Status == UserStatus.Active)
        {
            return Result.FromInvalid(
                DomainErrors.User.AlreadyActivated);
        }

        Status = UserStatus.Active;

        ActivedOnUtc = utcNow;

        AddEvent(new UserActivatedDomainEvent(Id));

        return Result.WithSuccess();
    }

    public Result Inactivate(DateTime utcNow)
    {
        if (Status == UserStatus.Inactive)
        {
            return Result.FromInvalid(
                DomainErrors.User.AlreadyInactivated);
        }

        Status = UserStatus.Inactive;

        InactivedOnUtc = utcNow;

        AddEvent(new UserInactivatedDomainEvent(Id));

        return Result.WithSuccess();
    }

    #endregion

    #region Email

    public Result AddEmail(EmailEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        _emails ??= [];

        if (ExistsEmailByValue(entity.Email))
        {
            return Result.FromInvalid(
                DomainErrors.Email.DuplicateEmail);
        }

        _emails.Add(entity);

        return Result.WithSuccess();
    }

    public Result RemoveEmail(EmailEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        _emails ??= [];

        if (!ExistsEmailByValue(entity.Email))
        {
            return Result.FromInvalid(
                DomainErrors.Email.NotFound);
        }

        _emails.Remove(entity);

        return Result.WithSuccess();
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
}
