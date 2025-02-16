using JacksonVeroneze.NET.DomainObjects.Domain;
using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseDomain.Entities.Base;
using TaxiService.Common.BaseDomain.ValueObjects;
using TaxiService.Modules.Passengers.Domain.Core.Errors;
using TaxiService.Modules.Passengers.Domain.Enums;
using TaxiService.Modules.Passengers.Domain.Events.Passanger;

namespace TaxiService.Modules.Passengers.Domain.Entities;

public class PassangerEntity : BaseEntityAggregateRoot, IAggregateRoot
{
    public Guid Id { get; private set; }

    public NameValueObject Name { get; private set; } = null!;

    public DateOnly Birthday { get; private set; }

    public Gender Gender { get; private set; }

    public CpfValueObject Cpf { get; private set; } = null!;

    public PassangerStatus Status { get; private set; }

    public DateTime? ActivedOnUtc { get; private set; }

    public DateTime? InactivedOnUtc { get; private set; }

    #region ctor

    public PassangerEntity()
    {
    }

    private PassangerEntity(NameValueObject name,
        DateOnly birthday, Gender gender,
        CpfValueObject cpf)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(cpf);

        Id = Guid.NewGuid();
        Name = name;
        Birthday = birthday;
        Gender = gender;
        Cpf = cpf;

        Status = PassangerStatus.PendingActivation;

        AddEvent(new PassangerCreatedDomainEvent(Id));
    }

    #endregion

    #region Status

    public Result Activate(DateTime utcNow)
    {
        if (IsActive)
        {
            return Result.FromInvalid(
                DomainErrors.PassangerError.AlreadyActivated);
        }

        Status = PassangerStatus.Active;

        ActivedOnUtc = utcNow;

        AddEvent(new PassangerActivatedDomainEvent(Id));

        return Result.WithSuccess();
    }

    public Result Inactivate(DateTime utcNow)
    {
        if (IsInactive)
        {
            return Result.FromInvalid(
                DomainErrors.PassangerError.AlreadyInactivated);
        }

        Status = PassangerStatus.Inactive;

        InactivedOnUtc = utcNow;

        AddEvent(new PassangerInactivatedDomainEvent(Id));

        return Result.WithSuccess();
    }

    public bool IsActive => Status is PassangerStatus.Active;
    public bool IsInactive => Status is PassangerStatus.Inactive;
    public bool IsPendingActivation => Status is PassangerStatus.PendingActivation;

    #endregion

    #region Factory

    // Factory Method
    public static Result<PassangerEntity> Create(string? name,
        DateOnly birthday, Gender gender, string? cpf)
    {
        Result<NameValueObject> nameVo = NameValueObject.Create(name);
        Result<CpfValueObject> cpfVo = CpfValueObject.Create(cpf);

        Result resultValidate = Result.FailuresOrSuccess(nameVo, cpfVo);

        if (resultValidate.IsFailure)
        {
            return Result<PassangerEntity>
                .FromInvalid(resultValidate.Errors!);
        }

        PassangerEntity entity = new(nameVo.Value!, birthday,
            gender, cpfVo.Value!);

        return Result<PassangerEntity>.WithSuccess(entity);
    }

    #endregion
}