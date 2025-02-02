using TaxiService.Modules.Payments.Domain.Entities;

namespace TaxiService.Modules.Payments.Application.v1.Interfaces.Repositories.Provider;

public interface IProviderReadRepository
{
    Task<IReadOnlyCollection<ProviderEntity>> GetAllAsync(
        CancellationToken cancellationToken);
}