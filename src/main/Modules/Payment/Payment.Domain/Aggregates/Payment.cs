using TaxiService.Common.SharedKernel;

namespace TaxiService.Modules.Payment.Domain.Aggregates;

public class Payment : BaseAggregateRoot
{
    public void SetCompleted()
    {
        throw new NotSupportedException();
    }
}
