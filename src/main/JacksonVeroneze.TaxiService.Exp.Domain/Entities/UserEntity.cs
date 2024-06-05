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
    public Guid Id { get; private set; }

    public NameValueObject Name { get; private set; } = null!;

    public DateOnly Birthday { get; private set; }

    public GenderType Gender { get; private set; }

    public CpfValueObject Cpf { get; private set; } = null!;

    public UserStatus Status { get; private set; }

    public DateTime? ActivedOnUtc { get; private set; }

    public DateTime? InactivedOnUtc { get; private set; }

    #region ctor

    public UserEntity()
    {
    }

    private UserEntity(NameValueObject name,
        DateOnly birthday, GenderType gender,
        CpfValueObject cpf)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(birthday);
        ArgumentNullException.ThrowIfNull(gender);
        ArgumentNullException.ThrowIfNull(cpf);

        Id = Guid.NewGuid();
        Name = name;
        Birthday = birthday;
        Gender = gender;
        Cpf = cpf;

        Status = UserStatus.PendingActivation;

        AddEvent(new UserCreatedDomainEvent(Id));
    }

    #endregion

    #region Status

    public Result Activate(DateTime utcNow)
    {
        if (IsActive)
        {
            return Result.FromInvalid(
                DomainErrors.UserError.AlreadyActivated);
        }

        Status = UserStatus.Active;

        ActivedOnUtc = utcNow;

        AddEvent(new UserActivatedDomainEvent(Id));

        return Result.WithSuccess();
    }

    public Result Inactivate(DateTime utcNow)
    {
        if (IsInactive)
        {
            return Result.FromInvalid(
                DomainErrors.UserError.AlreadyInactivated);
        }

        Status = UserStatus.Inactive;

        InactivedOnUtc = utcNow;

        AddEvent(new UserInactivatedDomainEvent(Id));

        return Result.WithSuccess();
    }

    public bool IsActive => Status is UserStatus.Active;
    public bool IsInactive => Status is UserStatus.Inactive;
    public bool IsPendingActivation => Status is UserStatus.PendingActivation;

    #endregion

    #region Factory

    // Factory Method
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

    #endregion
}