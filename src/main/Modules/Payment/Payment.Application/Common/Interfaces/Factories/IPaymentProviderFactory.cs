using System.Collections.Immutable;
using TaxiService.Modules.Payment.Application.Common.Interfaces.Providers;

namespace TaxiService.Modules.Payment.Application.Common.Interfaces.Factories;

public interface IPaymentProviderFactory
{
    Task<ImmutableArray<Lazy<IPaymentProvider>>> GetProvidersAsync(
        CancellationToken cancellationToken);
}
