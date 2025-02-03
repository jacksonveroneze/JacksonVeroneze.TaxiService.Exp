using Refit;
using TaxiService.Modules.Payments.Infrastructure.Acl.Models.MercadoPago;

namespace TaxiService.Modules.Payments.Infrastructure.DataProviders.HttpClients;

public interface IMercadoPago
{
    [Post("/api/v1/mercadopago-pay")]
    Task<PayResponse> SignAsync(
        [Body] PayRequest request,
        CancellationToken cancellationToken = default);
}