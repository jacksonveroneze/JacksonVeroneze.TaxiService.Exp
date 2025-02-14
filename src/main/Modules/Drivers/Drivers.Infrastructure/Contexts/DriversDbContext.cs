using Microsoft.EntityFrameworkCore;
using TaxiService.Common.BaseInfrastructure.Extensions;
using TaxiService.Modules.Drivers.Domain.Entities;

namespace TaxiService.Modules.Drivers.Infrastructure.Contexts;

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

        modelBuilder.ApplySoftDeleteQueryFilter<DriverAvailabilityEntity>();
        modelBuilder.ApplySoftDeleteQueryFilter<DriverEntity>();
        modelBuilder.ApplySoftDeleteQueryFilter<VehicleEntity>();

        modelBuilder.IgnoreClass();

        modelBuilder.HasDefaultSchema("driver");
    }
}