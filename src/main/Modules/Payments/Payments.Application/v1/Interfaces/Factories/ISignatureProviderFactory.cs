using TaxiService.Modules.Payments.Application.v1.Interfaces.Providers;

namespace TaxiService.Modules.Payments.Application.v1.Interfaces.Factories;

public interface IPaymentProviderFactory
{
    Task<IReadOnlyCollection<Lazy<IPaymentProvider>>> GetProvidersAsync(
        CancellationToken cancellationToken);
}