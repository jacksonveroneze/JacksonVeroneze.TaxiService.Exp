using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities.Base;
using JacksonVeroneze.TaxiService.Exp.Domain.ValueObjects;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Entities;

public class EmailEntity : BaseEntityAggregateRoot
{
    public virtual EmailValueObject Email { get; } = null!;

    public virtual UserEntity User { get; } = null!;

    protected EmailEntity()
    {
    }

    private EmailEntity(UserEntity user, EmailValueObject email)
    {
        ArgumentNullException.ThrowIfNull(user);
        ArgumentNullException.ThrowIfNull(email);

        User = user;
        Email = email;
    }

    public static Result<EmailEntity> Create(UserEntity user,
        string? value)
    {
        Result<EmailValueObject> emailVo = EmailValueObject.Create(value);

        if (emailVo.IsFailure)
        {
            return Result<EmailEntity>
                .FromInvalid(emailVo.Error!);
        }

        EmailEntity entity = new(user, emailVo.Value!);

        return Result<EmailEntity>.WithSuccess(entity);
    }
}
