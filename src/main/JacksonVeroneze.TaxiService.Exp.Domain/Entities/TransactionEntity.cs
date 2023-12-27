using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Domain.Core.Errors;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities.Base;
using JacksonVeroneze.TaxiService.Exp.Domain.Enums;
using JacksonVeroneze.TaxiService.Exp.Domain.ValueObjects;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Entities;

public class TransactionEntity : BaseEntityAggregateRoot
{
    public virtual RideEntity? Ride { get; }

    public MoneyValueObject? Ammount { get; }

    public DateTime Date { get; }

    public TransactionStatus Status { get; private set; }

    protected TransactionEntity()
    {
    }

    private TransactionEntity(
        RideEntity? ride, MoneyValueObject ammount)
    {
        ArgumentNullException.ThrowIfNull(ride);

        Ride = ride;
        Ammount = ammount;
        Date = DateTime.UtcNow;
        Status = TransactionStatus.WaitingPayment;
    }

    public static Result<TransactionEntity> Create(
        RideEntity ride, decimal ammount)
    {
        Result<MoneyValueObject> moneyVo = MoneyValueObject
            .Create(ammount);

        if (moneyVo.IsFailure)
        {
            return Result<TransactionEntity>
                .FromInvalid(moneyVo.Error!);
        }

        TransactionEntity entity = new(ride, moneyVo.Value!);

        return Result<TransactionEntity>
            .WithSuccess(entity);
    }

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
}