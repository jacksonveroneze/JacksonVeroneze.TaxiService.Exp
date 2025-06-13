using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseDomain;
using TaxiService.Common.BaseDomain.Entities.Base;
using TaxiService.Common.BaseDomain.ValueObjects;

namespace TaxiService.Modules.Passenger.Domain.Entities;

public class Email : BaseEntity
{
    public Guid Id { get; private set; }

    public EmailValueObject Value { get; private set; } = null!;

    public Guid PassengerId { get; private set; }

    #region ctor

    protected Email()
    {
    }

    private Email(
        Guid passengerId,
        EmailValueObject value)
    {
        ArgumentNullException.ThrowIfNull(value);

        Id = GuidGenerator.Generate();
        PassengerId = passengerId;
        Value = value;
    }

    #endregion

    #region Factory

    public static Result<Email> Create(Guid passengerId,
        string? value)
    {
        Result<EmailValueObject> emailVo = EmailValueObject.Create(value);

        if (emailVo.IsFailure)
        {
            return Result<Email>
                .FromInvalid(emailVo.FirstError!);
        }

        Email entity = new(passengerId, emailVo.Value!);

        return Result<Email>.WithSuccess(entity);
    }

    #endregion
}
