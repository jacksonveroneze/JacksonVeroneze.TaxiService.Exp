using Microsoft.EntityFrameworkCore;
using TaxiService.Common.BaseInfrastructure.Extensions;
using TaxiService.Modules.Drivers.Domain.Entities;
using TaxiService.Modules.Drivers.Infrastructure.Mappings;
using TaxiService.Modules.Drivers.Infrastructure.Mappings.Domain;

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

        modelBuilder.ApplyConfiguration(new DriverAvailabilityMapping());
        modelBuilder.ApplyConfiguration(new DriverLocationMapping());
        modelBuilder.ApplyConfiguration(new DriverMapping());
        modelBuilder.ApplyConfiguration(new VehicleEntityMapping());
        
        modelBuilder.ApplySoftDeleteQueryFilter<DriverAvailabilityEntity>();
        modelBuilder.ApplySoftDeleteQueryFilter<DriverEntity>();
        modelBuilder.ApplySoftDeleteQueryFilter<VehicleEntity>();

        modelBuilder.IgnoreClass();

        modelBuilder.HasDefaultSchema("driver");
    }
}