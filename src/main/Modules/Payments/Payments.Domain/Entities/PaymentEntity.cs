using TaxiService.Common.BaseDomain.Entities.Base;

namespace TaxiService.Modules.Payments.Domain.Entities;

public class PaymentEntity : BaseEntityAggregateRoot
{

    public void SetCompleted()
    {
        throw new NotSupportedException();
    }
}