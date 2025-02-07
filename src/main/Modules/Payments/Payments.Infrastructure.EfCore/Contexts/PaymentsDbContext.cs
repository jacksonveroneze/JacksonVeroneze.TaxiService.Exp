using Microsoft.EntityFrameworkCore;
using TaxiService.Common.BaseInfrastructure.Extensions;
using TaxiService.Modules.Payments.Domain.Entities;

namespace TaxiService.Modules.Payments.Infrastructure.EfCore.Contexts;

[ExcludeFromCodeCoverage]
public class PaymentsDbContext(
    DbContextOptions<PaymentsDbContext> options)
    : DbContext(options)
{
    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(PaymentsDbContext).Assembly);

        modelBuilder.ApplySoftDeleteQueryFilter<ProviderEntity>();
        modelBuilder.ApplySoftDeleteQueryFilter<PaymentEntity>();

        modelBuilder.IgnoreClass();

        modelBuilder.HasDefaultSchema("payment");
    }
}