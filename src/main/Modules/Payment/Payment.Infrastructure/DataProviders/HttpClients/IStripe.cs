using Refit;
using TaxiService.Modules.Payment.Infrastructure.Acl.Models.Stripe;

namespace TaxiService.Modules.Payment.Infrastructure.DataProviders.HttpClients;

public interface IStripe
{
    [Post("/api/v1/stripe-pay")]
    Task<PayResponse> SignAsync(
        [Body] PayRequest request,
        CancellationToken cancellationToken);
}
