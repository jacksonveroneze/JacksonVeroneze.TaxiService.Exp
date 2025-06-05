using System.Collections.Immutable;

namespace TaxiService.Modules.Payment.Application.Common.Interfaces.Repositories;

public interface IProviderReadRepository
{
    Task<ImmutableArray<Domain.Aggregates.Provider>> GetAllAsync(
        CancellationToken cancellationToken);
}
