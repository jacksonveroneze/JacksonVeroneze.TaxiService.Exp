using Refit;
using TaxiService.Modules.Payments.Infrastructure.Acl.Models.Stripe;

namespace TaxiService.Modules.Payments.Infrastructure.DataProviders.HttpClients;

public interface IStripe
{
    [Post("/api/v1/stripe-pay")]
    Task<PayResponse> SignAsync(
        [Body] PayRequest request,
        CancellationToken cancellationToken);
}