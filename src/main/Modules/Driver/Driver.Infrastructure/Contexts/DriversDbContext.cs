using Microsoft.EntityFrameworkCore;
using TaxiService.Common.BaseInfrastructure.Extensions;
using TaxiService.Modules.Driver.Domain.Aggregates;
using TaxiService.Modules.Driver.Domain.Entities;
using TaxiService.Modules.Driver.Infrastructure.Mappings.Domain;

namespace TaxiService.Modules.Driver.Infrastructure.Contexts;

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

        modelBuilder.ApplySoftDeleteQueryFilter<DriverAvailabilityHistory>();
        modelBuilder.ApplySoftDeleteQueryFilter<DriverLocationHistory>();
        modelBuilder.ApplySoftDeleteQueryFilter<Domain.Aggregates.Driver>();
        modelBuilder.ApplySoftDeleteQueryFilter<Vehicle>();

        modelBuilder.IgnoreClass();

        modelBuilder.HasDefaultSchema(Constants.SchemaName);
    }
}
