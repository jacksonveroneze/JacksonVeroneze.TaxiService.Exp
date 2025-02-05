using JacksonVeroneze.NET.DomainObjects.Domain;
using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseDomain.Entities.Base;
using TaxiService.Common.BaseDomain.ValueObjects;
using TaxiService.Modules.Drivers.Domain.Core.Errors;
using TaxiService.Modules.Drivers.Domain.Enums;

namespace TaxiService.Modules.Drivers.Domain.Entities;

public class DriverEntity : BaseEntityAggregateRoot, IAggregateRoot
{
    public Guid Id { get; private set; }

    public NameValueObject Name { get; private set; } = null!;

    public DateOnly Birthday { get; private set; }

    public Gender Gender { get; private set; }

    public CpfValueObject Cpf { get; private set; } = null!;

    public DriverStatus Status { get; private set; }

    public DateTime? ActivedOnUtc { get; private set; }

    public DateTime? InactivedOnUtc { get; private set; }

    #region ctor

    public DriverEntity()
    {
    }

    private DriverEntity(NameValueObject name,
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
        DateOnly birthday, Gender gender, string? cpf)
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
            gender, cpfVo.Value!);

        return Result<DriverEntity>.WithSuccess(entity);
    }

    #endregion
}