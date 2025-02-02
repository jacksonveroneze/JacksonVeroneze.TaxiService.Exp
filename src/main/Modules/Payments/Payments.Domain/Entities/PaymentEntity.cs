using TaxiService.Common.BaseDomain.Entities.Base;

namespace TaxiService.Modules.Payments.Domain.Entities;

public class PaymentEntity : BaseEntityAggregateRoot
{
    public Guid Id { get; private set; }

    public void SetCompleted()
    {
    }
}