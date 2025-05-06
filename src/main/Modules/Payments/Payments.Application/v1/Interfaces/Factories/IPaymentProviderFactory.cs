using System.Collections.Immutable;
using TaxiService.Modules.Payments.Application.v1.Interfaces.Providers;

namespace TaxiService.Modules.Payments.Application.v1.Interfaces.Factories;

public interface IPaymentProviderFactory
{
    Task<ImmutableArray<Lazy<IPaymentProvider>>> GetProvidersAsync(
        CancellationToken cancellationToken);
}