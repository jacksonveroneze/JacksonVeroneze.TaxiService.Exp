using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseDomain.Entities.Base;
using TaxiService.Common.BaseDomain.ValueObjects;

namespace TaxiService.Modules.Drivers.Domain.Entities;

public class EmailEntity : BaseEntityAggregateRoot
{
    public Guid Id { get; private set; }

    public EmailValueObject Email { get; private set; } = null!;

    public Guid DriverId { get; private set; }

    #region ctor

    public EmailEntity()
    {
    }

    private EmailEntity(
        Guid driverId,
        EmailValueObject email)
    {
        ArgumentNullException.ThrowIfNull(email);

        Id = Guid.NewGuid();
        DriverId = driverId;
        Email = email;
    }

    #endregion

    #region Factory

    public static Result<EmailEntity> Create(Guid driverId,
        string? value)
    {
        Result<EmailValueObject> emailVo = EmailValueObject.Create(value);

        if (emailVo.IsFailure)
        {
            return Result<EmailEntity>
                .FromInvalid(emailVo.Error!);
        }

        EmailEntity entity = new(driverId, emailVo.Value!);

        return Result<EmailEntity>.WithSuccess(entity);
    }

    #endregion
}