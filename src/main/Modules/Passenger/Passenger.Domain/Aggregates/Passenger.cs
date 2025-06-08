using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseDomain;
using TaxiService.Common.BaseDomain.Entities.Base;
using TaxiService.Common.BaseDomain.Enums;
using TaxiService.Common.BaseDomain.ValueObjects;
using TaxiService.Modules.Passenger.Domain.Core.Errors;
using TaxiService.Modules.Passenger.Domain.Enums;
using TaxiService.Modules.Passenger.Domain.Events;

namespace TaxiService.Modules.Passenger.Domain.Aggregates;

public class Passenger : BaseAggregateRoot
{
    public Guid Id { get; }

    public NameValueObject Name { get; private set; } = null!;

    public DateOnly Birthday { get; private set; }

    public Gender Gender { get; private set; }

    public CpfValueObject Cpf { get; private set; } = null!;

    public PassengerStatus Status { get; private set; }

    public DateTime? ActivedOnUtc { get; private set; }

    public DateTime? InactivedOnUtc { get; private set; }

    #region ctor

    public Passenger()
    {
    }

    private Passenger(NameValueObject name,
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

    public static Result<Passenger> Create(
        string? name, DateOnly birthday,
        Gender gender, string? cpf)
    {
        Result<NameValueObject> nameVo = NameValueObject.Create(name);
        Result<CpfValueObject> cpfVo = CpfValueObject.Create(cpf);

        Result resultValidate = Result.FailuresOrSuccess(nameVo, cpfVo);

        if (resultValidate.IsFailure)
        {
            return Result<Passenger>
                .FromInvalid(resultValidate.Errors);
        }

        Passenger entity = new(nameVo.Value!, birthday,
            gender, cpfVo.Value!);

        return Result<Passenger>.WithSuccess(entity);
    }

    #endregion
}
