using System.Collections.Immutable;
using TaxiService.Modules.Payment.Domain.Aggregates;

namespace TaxiService.Modules.Payment.Application.Common.Interfaces.Repositories;

public interface IProviderReadRepository
{
    Task<ImmutableArray<Provider>> GetAllAsync(
        CancellationToken cancellationToken);
}
