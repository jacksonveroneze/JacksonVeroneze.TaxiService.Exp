using TaxiService.Common.BaseDomain.Entities.Base;

namespace TaxiService.Modules.Payments.Domain.Entities;

public class PaymentEntity : BaseAggregateRoot
{

    public void SetCompleted()
    {
        throw new NotSupportedException();
    }
}