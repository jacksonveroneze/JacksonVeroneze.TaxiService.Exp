using TaxiService.Common.BaseDomain.Entities.Base;
using TaxiService.Modules.Payment.Domain.Enums;

namespace TaxiService.Modules.Payment.Domain.Aggregates;

public class Provider : BaseAggregateRoot
{
    public PaymentProviderType ProviderName { get; init; }

    public bool IsActive { get; init; }

    public int Priority { get; init; }
}
