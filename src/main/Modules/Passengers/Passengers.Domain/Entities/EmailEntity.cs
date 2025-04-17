using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseDomain;
using TaxiService.Common.BaseDomain.Entities.Base;
using TaxiService.Common.BaseDomain.ValueObjects;

namespace TaxiService.Modules.Passengers.Domain.Entities;

public class EmailEntity : BaseEntityAggregateRoot
{
    public Guid Id { get; private set; }

    public EmailValueObject Email { get; private set; } = null!;

    public Guid PassangerId { get; private set; }

    #region ctor

    public EmailEntity()
    {
    }

    private EmailEntity(
        Guid passangerId,
        EmailValueObject email)
    {
        ArgumentNullException.ThrowIfNull(email);

        Id = GuidGenerator.Generate();
        PassangerId = passangerId;
        Email = email;
    }

    #endregion

    #region Factory

    public static Result<EmailEntity> Create(Guid passangerId,
        string? value)
    {
        Result<EmailValueObject> emailVo = EmailValueObject.Create(value);

        if (emailVo.IsFailure)
        {
            return Result<EmailEntity>
                .FromInvalid(emailVo.Error!);
        }

        EmailEntity entity = new(passangerId, emailVo.Value!);

        return Result<EmailEntity>.WithSuccess(entity);
    }

    #endregion
}