using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Domain.Core.Errors;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities.Base;
using JacksonVeroneze.TaxiService.Exp.Domain.Enums;
using JacksonVeroneze.TaxiService.Exp.Domain.ValueObjects;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Entities;

public class TransactionEntity : BaseEntityAggregateRoot
{
    public Guid RideId { get; }

    public MoneyValueObject Ammount { get; } = null!;

    public DateTime Date { get; }

    public TransactionStatus Status { get; private set; }

    #region ctor

    protected TransactionEntity()
    {
    }

    private TransactionEntity(
        Guid rideId, MoneyValueObject ammount)
    {
        RideId = rideId;
        Ammount = ammount;
        Date = DateTime.UtcNow;
        Status = TransactionStatus.WaitingPayment;
        Ammount = new MoneyValueObject(0);
    }

    #endregion

    #region Pay

    public Result Pay()
    {
        if (Status == TransactionStatus.Paid)
        {
            return Result.FromInvalid(
                DomainErrors.TransactionError.StatusAlreadyPaid);
        }

        if (Status != TransactionStatus.WaitingPayment)
        {
            return Result.FromInvalid(
                DomainErrors.TransactionError.InvalidStatusSetPaid);
        }

        Status = TransactionStatus.Paid;

        return Result.WithSuccess();
    }

    #endregion

    #region Factory

    public static Result<TransactionEntity> Create(
        Guid rideId, decimal ammount)
    {
        Result<MoneyValueObject> moneyVo = MoneyValueObject
            .Create(ammount);

        if (moneyVo.IsFailure)
        {
            return Result<TransactionEntity>
                .FromInvalid(moneyVo.Error!);
        }

        TransactionEntity entity = new(rideId, moneyVo.Value!);

        return Result<TransactionEntity>
            .WithSuccess(entity);
    }

    #endregion
}