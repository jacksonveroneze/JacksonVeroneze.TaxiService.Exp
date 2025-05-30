using JacksonVeroneze.NET.DomainObjects.Domain;
using JacksonVeroneze.NET.Result;

using TaxiService.Common.BaseDomain;
using TaxiService.Common.BaseDomain.Entities.Base;
using TaxiService.Common.BaseDomain.ValueObjects;
using TaxiService.Modules.Passengers.Domain.Core.Errors;
using TaxiService.Modules.Passengers.Domain.Enums;
using TaxiService.Modules.Passengers.Domain.Events.Passenger;

namespace TaxiService.Modules.Passengers.Domain.Entities;

public class PassengerEntity : BaseEntityAggregateRoot, IAggregateRoot
{
    public Guid Id { get; private set; }

    public NameValueObject Name { get; private set; } = null!;

    public DateOnly Birthday { get; private set; }

    public Gender Gender { get; private set; }

    public CpfValueObject Cpf { get; private set; } = null!;

    public PassengerStatus Status { get; private set; }

    public DateTime? ActivedOnUtc { get; private set; }

    public DateTime? InactivedOnUtc { get; private set; }

    #region ctor

    public PassengerEntity()
    {
    }

    private PassengerEntity(NameValueObject name,
        DateOnly birthday, Gender gender,
        CpfValueObject cpf)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(cpf);

        Id = GuidGenerator.Generate();
        Name = name;
        Birthday = birthday;
        Gender = gender;
        Cpf = cpf;

        Status = PassengerStatus.PendingActivation;

        AddEvent(new PassengerCreatedDomainEvent(Id));
    }

    #endregion

    #region Status

    public Result Activate(DateTime utcNow)
    {
        if (IsActive)
        {
            return Result.FromInvalid(
                DomainErrors.PassengerError.AlreadyActivated);
        }

        Status = PassengerStatus.Active;

        ActivedOnUtc = utcNow;

        AddEvent(new PassengerActivatedDomainEvent(Id));

        return Result.WithSuccess();
    }

    public Result Inactivate(DateTime utcNow)
    {
        if (IsInactive)
        {
            return Result.FromInvalid(
                DomainErrors.PassengerError.AlreadyInactivated);
        }

        Status = PassengerStatus.Inactive;

        InactivedOnUtc = utcNow;

        AddEvent(new PassengerInactivatedDomainEvent(Id));

        return Result.WithSuccess();
    }

    public bool IsActive => Status is PassengerStatus.Active;
    public bool IsInactive => Status is PassengerStatus.Inactive;
    public bool IsPendingActivation => Status is PassengerStatus.PendingActivation;

    #endregion

    #region Factory

    public static Result<PassengerEntity> Create(
        string? name, DateOnly birthday,
        Gender gender, string? cpf)
    {
        Result<NameValueObject> nameVo = NameValueObject.Create(name);
        Result<CpfValueObject> cpfVo = CpfValueObject.Create(cpf);

        Result resultValidate = Result.FailuresOrSuccess(nameVo, cpfVo);

        if (resultValidate.IsFailure)
        {
            return Result<PassengerEntity>
                .FromInvalid(resultValidate.Errors);
        }

        PassengerEntity entity = new(nameVo.Value!, birthday,
            gender, cpfVo.Value!);

        return Result<PassengerEntity>.WithSuccess(entity);
    }

    #endregion
}
