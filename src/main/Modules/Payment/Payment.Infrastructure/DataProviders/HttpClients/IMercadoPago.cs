using Refit;
using TaxiService.Modules.Payment.Infrastructure.Acl.Models.MercadoPago;

namespace TaxiService.Modules.Payment.Infrastructure.DataProviders.HttpClients;

public interface IMercadoPago
{
    [Post("/api/v1/mercadopago-pay")]
    Task<PayResponse> SignAsync(
        [Body] PayRequest request,
        CancellationToken cancellationToken);
}
