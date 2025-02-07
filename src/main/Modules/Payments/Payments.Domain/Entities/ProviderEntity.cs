using TaxiService.Common.BaseDomain.Entities.Base;
using TaxiService.Modules.Payments.Domain.Enums;

namespace TaxiService.Modules.Payments.Domain.Entities;

public class ProviderEntity : BaseEntityAggregateRoot
{
    public PaymentProviderType ProviderName { get; init; }
    
    public bool IsActive { get; init; }
    
    public int Priority { get; init; }
}