using System.Collections.Immutable;
using JacksonVeroneze.NET.EntityFramework.Interfaces;
using TaxiService.Modules.Payments.Application.v1.Interfaces.Repositories.Provider;
using TaxiService.Modules.Payments.Domain.Entities;
using TaxiService.Modules.Payments.Infrastructure.Contexts;

namespace TaxiService.Modules.Payments.Infrastructure.Repositories.Provider;

internal sealed class ProviderReadRepository(
    IEfCoreRepository<ProviderEntity, PaymentsDbContext> service)
    : IProviderReadRepository
{
    public async Task<ImmutableArray<ProviderEntity>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        var providers = await service.GetAllAsync(
            where => where.IsActive,
            order => order.Priority,
            10,
            cancellationToken);

        return [..providers];
    }
}
