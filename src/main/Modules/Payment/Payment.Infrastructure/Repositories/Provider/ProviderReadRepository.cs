using System.Collections.Immutable;
using JacksonVeroneze.NET.EntityFramework.Interfaces;
using TaxiService.Modules.Payment.Application.Common.Interfaces.Repositories;
using TaxiService.Modules.Payment.Infrastructure.Contexts;

namespace TaxiService.Modules.Payment.Infrastructure.Repositories.Provider;

internal sealed class ProviderReadRepository(
    IEfCoreRepository<Domain.Aggregates.Provider, PaymentsDbContext> service)
    : IProviderReadRepository
{
    public async Task<ImmutableArray<Domain.Aggregates.Provider>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        ICollection<Domain.Aggregates.Provider> providers =
            await service.GetAllAsync(
                where => where.IsActive,
                order => order.Priority,
                10,
                cancellationToken);

        return [..providers];
    }
}
