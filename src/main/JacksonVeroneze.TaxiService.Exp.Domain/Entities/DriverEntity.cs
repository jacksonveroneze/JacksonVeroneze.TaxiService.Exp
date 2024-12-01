using JacksonVeroneze.NET.DomainObjects.Domain;
using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Domain.Core.Errors;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities.Base;
using JacksonVeroneze.TaxiService.Exp.Domain.Enums;
using JacksonVeroneze.TaxiService.Exp.Domain.ValueObjects;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Entities;

public class DriverEntity : BaseEntityAggregateRoot, IAggregateRoot
{
    public Guid Id { get; private set; }

    public NameValueObject Name { get; private set; } = null!;

    public DateOnly Birthday { get; private set; }

    public GenderType Gender { get; private set; }

    public CpfValueObject Cpf { get; private set; } = null!;

    public DriverStatus Status { get; private set; }

    public DateTime? ActivedOnUtc { get; private set; }

    public DateTime? InactivedOnUtc { get; private set; }

    #region ctor

    public DriverEntity()
    {
    }

    private DriverEntity(NameValueObject name,
        DateOnly birthday, GenderType gender,
        CpfValueObject cpf)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(cpf);

        Id = Guid.NewGuid();
        Name = name;
        Birthday = birthday;
        Gender = gender;
        Cpf = cpf;

        Status = DriverStatus.PendingActivation;
    }

    #endregion

    #region Status

    public Result Activate(DateTime utcNow)
    {
        if (IsActive)
        {
            return Result.FromInvalid(
                DomainErrors.DriverError.AlreadyActivated);
        }

        Status = DriverStatus.Active;

        ActivedOnUtc = utcNow;

        return Result.WithSuccess();
    }

    public Result Inactivate(DateTime utcNow)
    {
        if (IsInactive)
        {
            return Result.FromInvalid(
                DomainErrors.DriverError.AlreadyInactivated);
        }

        Status = DriverStatus.Inactive;

        InactivedOnUtc = utcNow;

        return Result.WithSuccess();
    }

    public bool IsActive => Status is DriverStatus.Active;
    public bool IsInactive => Status is DriverStatus.Inactive;
    public bool IsPendingActivation => Status is DriverStatus.PendingActivation;

    #endregion

    #region Factory

    // Factory Method
    public static Result<DriverEntity> Create(string? name,
        DateOnly birthday, GenderType genderType, string? cpf)
    {
        Result<NameValueObject> nameVo = NameValueObject.Create(name);
        Result<CpfValueObject> cpfVo = CpfValueObject.Create(cpf);

        Result resultValidate = Result.FailuresOrSuccess(nameVo, cpfVo);

        if (resultValidate.IsFailure)
        {
            return Result<DriverEntity>
                .FromInvalid(resultValidate.Errors!);
        }

        DriverEntity entity = new(nameVo.Value!, birthday,
            genderType, cpfVo.Value!);

        return Result<DriverEntity>.WithSuccess(entity);
    }

    #endregion
}