using Microsoft.EntityFrameworkCore;
using TaxiService.Common.BaseInfrastructure.Extensions;
using TaxiService.Modules.Drivers.Domain.Entities;

namespace TaxiService.Modules.Drivers.Infrastructure.EfCore.Contexts;

[ExcludeFromCodeCoverage]
public class DriversDbContext(
    DbContextOptions<DriversDbContext> options)
    : DbContext(options)
{
    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(DriversDbContext).Assembly);

        modelBuilder.ApplySoftDeleteQueryFilter<EmailEntity>();
        modelBuilder.ApplySoftDeleteQueryFilter<DriverEntity>();

        modelBuilder.IgnoreClass();

        modelBuilder.HasDefaultSchema("driver");
    }
}