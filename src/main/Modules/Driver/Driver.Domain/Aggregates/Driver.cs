using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseDomain;
using TaxiService.Common.BaseDomain.Entities.Base;
using TaxiService.Common.BaseDomain.Enums;
using TaxiService.Common.BaseDomain.ValueObjects;
using TaxiService.Modules.Driver.Domain.Core.Errors;
using TaxiService.Modules.Driver.Domain.Enums;
using TaxiService.Modules.Driver.Domain.Events;

namespace TaxiService.Modules.Driver.Domain.Aggregates;

public class Driver : BaseAggregateRoot
{
    public Guid Id { get; private set; }

    public NameValueObject Name { get; private set; } = null!;

    public DateOnly Birthday { get; private set; }

    public EmailValueObject Email { get; private set; } = null!;

    public PhoneValueObject Phone { get; private set; } = null!;

    public CpfValueObject Cpf { get; private set; } = null!;

    public Gender Gender { get; private set; }

    public DriverStatus Status { get; private set; }

    public DateTime? ActivedOnUtc { get; private set; }

    public DateTime? InactivedOnUtc { get; private set; }

    #region ctor

    protected Driver()
    {
    }

    private Driver(NameValueObject name,
        DateOnly birthday, EmailValueObject email,
        PhoneValueObject phone, CpfValueObject cpf,
        Gender gender)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(email);
        ArgumentNullException.ThrowIfNull(phone);
        ArgumentNullException.ThrowIfNull(cpf);

        Id = GuidGenerator.Generate();
        Name = name;
        Birthday = birthday;
        Email = email;
        Phone = phone;
        Cpf = cpf;
        Gender = gender;
        Status = DriverStatus.PendingActivation;

        AddEvent(new DriverCreatedEvent(
            Id, Name.Value!, Email.Value!, Cpf.Value!));
    }

    #endregion

    #region Status

    public Result Activate(DateTime utcNow)
    {
        if (!IsPendingActivation)
        {
            return Result.FromRuleViolation(
                DomainErrors.DriverErrors.NotPendingApproval);
        }

        Status = DriverStatus.Active;
        ActivedOnUtc = utcNow;
        AddEvent(new DriverActivatedEvent(Id, utcNow));

        return Result.WithSuccess();
    }

    public Result Inactivate(DateTime utcNow)
    {
        if (IsBlockedPermanent)
        {
            return Result.FromRuleViolation(
                DomainErrors.DriverErrors.AlreadyBlockPermanently);
        }

        if (IsInactive)
        {
            return Result.FromRuleViolation(
                DomainErrors.DriverErrors.AlreadyInactivated);
        }

        Status = DriverStatus.Inactive;
        InactivedOnUtc = utcNow;
        AddEvent(new DriverInactivedEvent(Id, utcNow));

        return Result.WithSuccess();
    }

    public Result Reactivate()
    {
        if (IsActive)
        {
            return Result.FromRuleViolation(
                DomainErrors.DriverErrors.AlreadyActive);
        }

        if (IsBlockedPermanent)
        {
            return Result.FromRuleViolation(
                DomainErrors.DriverErrors.AlreadyBlockPermanently);
        }

        Status = DriverStatus.Active;
        AddEvent(new DriverReactivatedEvent(Id, DateTime.UtcNow));

        return Result.WithSuccess();
    }

    public Result BlockTemporarily()
    {
        if (IsBlockedPermanent)
        {
            return Result.FromRuleViolation(
                DomainErrors.DriverErrors.AlreadyBlockPermanently);
        }

        if (IsBlockedTemporarily)
        {
            return Result.FromRuleViolation(
                DomainErrors.DriverErrors.AlreadyBlockedTemporarily);
        }

        Status = DriverStatus.BlockedTemporarily;
        AddEvent(new DriverBlockedTemporarilyEvent(Id, DateTime.UtcNow));

        return Result.WithSuccess();
    }

    public Result BlockPermanently()
    {
        if (IsBlockedPermanent)
        {
            return Result.FromRuleViolation(
                DomainErrors.DriverErrors.AlreadyBlockPermanently);
        }

        Status = DriverStatus.BlockedPermanent;
        AddEvent(new DriverBlockedPermanentEvent(Id, DateTime.UtcNow));

        return Result.WithSuccess();
    }

    public Result Remove()
    {
        if (IsBlockedPermanent)
        {
            return Result.FromRuleViolation(
                DomainErrors.DriverErrors.AlreadyBlockPermanently);
        }

        if (IsBlockedTemporarily)
        {
            return Result.FromRuleViolation(
                DomainErrors.DriverErrors.AlreadyBlockedTemporarily);
        }

        AddEvent(new DriverRemovedEvent(Id, DateTime.UtcNow));

        return Result.WithSuccess();
    }

    public bool IsPendingActivation =>
        Status is DriverStatus.PendingActivation;

    public bool IsActive =>
        Status is DriverStatus.Active;

    public bool IsInactive =>
        Status is DriverStatus.Inactive;

    public bool IsBlockedTemporarily =>
        Status is DriverStatus.BlockedTemporarily;

    public bool IsBlockedPermanent =>
        Status is DriverStatus.BlockedPermanent;

    #endregion

    #region Factory

    public static Result<Driver> Create(
        string? name, DateOnly birthday,
        string email, string phone,
        string? cpf, Gender gender)
    {
        Result<NameValueObject> nameVo = NameValueObject.Create(name);
        Result<EmailValueObject> emailVo = EmailValueObject
            .Create(email.ToLowerInvariant());
        Result<PhoneValueObject> phoneVo = PhoneValueObject.Create(phone);
        Result<CpfValueObject> cpfVo = CpfValueObject.Create(cpf);

        Result resultValidate = Result.FailuresOrSuccess(
            nameVo, phoneVo, cpfVo, emailVo);

        if (resultValidate.IsFailure)
        {
            return Result<Driver>
                .FromInvalid(resultValidate.Errors);
        }

        Driver entity = new(nameVo.Value!,
            birthday, emailVo.Value!, phoneVo.Value!,
            cpfVo.Value!, gender);

        return Result<Driver>.WithSuccess(entity);
    }

    #endregion
}
