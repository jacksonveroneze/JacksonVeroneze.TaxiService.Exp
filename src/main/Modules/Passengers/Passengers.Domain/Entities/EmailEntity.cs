using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseDomain.Entities.Base;
using TaxiService.Modules.Passengers.Domain.ValueObjects;

namespace TaxiService.Modules.Passengers.Domain.Entities;

public class EmailEntity : BaseEntityAggregateRoot
{
    public Guid Id { get; private set; }

    public EmailValueObject Email { get; private set; } = null!;

    public Guid UserId { get; private set; }

    #region ctor

    public EmailEntity()
    {
    }

    private EmailEntity(
        Guid userId,
        EmailValueObject email)
    {
        ArgumentNullException.ThrowIfNull(email);

        Id = Guid.NewGuid();
        UserId = userId;
        Email = email;
    }

    #endregion

    #region Factory

    public static Result<EmailEntity> Create(Guid userId,
        string? value)
    {
        Result<EmailValueObject> emailVo = EmailValueObject.Create(value);

        if (emailVo.IsFailure)
        {
            return Result<EmailEntity>
                .FromInvalid(emailVo.Error!);
        }

        EmailEntity entity = new(userId, emailVo.Value!);

        return Result<EmailEntity>.WithSuccess(entity);
    }

    #endregion
}