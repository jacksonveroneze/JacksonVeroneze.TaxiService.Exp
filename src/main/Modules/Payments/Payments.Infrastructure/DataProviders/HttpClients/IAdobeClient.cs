using Refit;
using TaxiService.Modules.Payments.Infrastructure.Acl.Models.Adobe;

namespace TaxiService.Modules.Payments.Infrastructure.DataProviders.HttpClients;

public interface IAdobeClient
{
    [Post("/api/v1/adobe-sign-document")]
    Task<SignResponse> SignAsync(
        [Body] SignRequest request,
        CancellationToken cancellationToken = default);
}