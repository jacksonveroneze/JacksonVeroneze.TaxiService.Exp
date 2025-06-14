using Microsoft.EntityFrameworkCore;
using TaxiService.Common.BaseInfrastructure.Extensions;
using TaxiService.Modules.Payment.Domain.Aggregates;

namespace TaxiService.Modules.Payment.Infrastructure.Contexts;

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

        modelBuilder.ApplySoftDeleteQueryFilter<Provider>();
        modelBuilder.ApplySoftDeleteQueryFilter<Domain.Aggregates.Payment>();

        modelBuilder.IgnoreClass();

        modelBuilder.HasDefaultSchema(Constants.SchemaName);
    }
}
