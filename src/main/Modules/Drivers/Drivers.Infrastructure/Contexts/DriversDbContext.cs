using Microsoft.EntityFrameworkCore;
using TaxiService.Common.BaseInfrastructure.Extensions;
using TaxiService.Modules.Drivers.Domain.Aggregates;
using TaxiService.Modules.Drivers.Domain.Entities;
using TaxiService.Modules.Drivers.Infrastructure.Mappings.Domain;

namespace TaxiService.Modules.Drivers.Infrastructure.Contexts;

[ExcludeFromCodeCoverage]
public class DriversDbContext(
    DbContextOptions<DriversDbContext> options)
    : DbContext(options)
{
    private const string Schema = "driver";

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.ApplyConfiguration(new DriverAvailabilityMapping());
        modelBuilder.ApplyConfiguration(new DriverLocationMapping());
        modelBuilder.ApplyConfiguration(new DriverMapping());
        modelBuilder.ApplyConfiguration(new VehicleEntityMapping());

        modelBuilder.ApplySoftDeleteQueryFilter<DriverAvailabilityHistory>();
        modelBuilder.ApplySoftDeleteQueryFilter<Driver>();
        modelBuilder.ApplySoftDeleteQueryFilter<Vehicle>();

        modelBuilder.IgnoreClass();

        modelBuilder.HasDefaultSchema(Schema);
    }
}
