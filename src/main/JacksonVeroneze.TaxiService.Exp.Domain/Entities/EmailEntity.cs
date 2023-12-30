using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities.Base;
using JacksonVeroneze.TaxiService.Exp.Domain.ValueObjects;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Entities;

public class EmailEntity : BaseEntityAggregateRoot
{
    public EmailValueObject Email { get; } = null!;

    public Guid UserId { get; }

    #region ctor

    protected EmailEntity()
    {
    }

    private EmailEntity(Guid userId,
        EmailValueObject email)
    {
        ArgumentNullException.ThrowIfNull(email);

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