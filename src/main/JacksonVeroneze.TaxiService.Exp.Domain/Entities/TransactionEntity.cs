using System.Text;
using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Domain.Core.Errors;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities.Base;
using JacksonVeroneze.TaxiService.Exp.Domain.Enums;
using JacksonVeroneze.TaxiService.Exp.Domain.ValueObjects;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Entities;

public class TransactionEntity : BaseEntityAggregateRoot
{
    public Guid Id { get; private set; }

    public Guid RideId { get; private set; }

    public MoneyValueObject Ammount { get; private set; } = null!;

    public DateTime Date { get; private set; }

    public TransactionStatus Status { get; private set; }

    #region ctor

    protected TransactionEntity()
    {
    }

    private TransactionEntity(
        Guid rideId, MoneyValueObject ammount)
    {
        Id = Guid.NewGuid();
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
        if (IsPaid)
        {
            return Result.FromInvalid(
                DomainErrors.TransactionError.StatusAlreadyPaid);
        }

        if (!IsWaitingPayment)
        {
            return Result.FromInvalid(
                DomainErrors.TransactionError.InvalidStatusSetPaid);
        }

        Status = TransactionStatus.Paid;

        return Result.WithSuccess();
    }

    #endregion

    #region Status

    public bool IsWaitingPayment => Status is TransactionStatus.WaitingPayment;
    public bool IsPaid => Status is TransactionStatus.Paid;

    #endregion

    #region Factory

    public static Result<TransactionEntity> Create(
        Guid rideId, decimal ammount)
    {
        StringBuilder builder = new();
        builder.Append("aa");

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