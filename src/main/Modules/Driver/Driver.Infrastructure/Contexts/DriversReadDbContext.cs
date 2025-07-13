using Microsoft.EntityFrameworkCore;
using TaxiService.Modules.Driver.Application.v1.Driver.Common.Models;
using TaxiService.Modules.Driver.Application.v1.DriverAvailability.Common.Models;
using TaxiService.Modules.Driver.Application.v1.DriverLocation.Common.Models;
using TaxiService.Modules.Driver.Application.v1.Vehicle.Common.Models;
using TaxiService.Modules.Driver.Infrastructure.Mappings.ReadModels;

namespace TaxiService.Modules.Driver.Infrastructure.Contexts;

[ExcludeFromCodeCoverage]
public class DriversReadDbContext(
    DbContextOptions<DriversReadDbContext> options)
    : DbContext(options)
{
    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.ApplyConfiguration(new DriverDtoMapping());
        modelBuilder.ApplyConfiguration(new DriverAvailabilityDtoMapping());
        modelBuilder.ApplyConfiguration(new DriverLocationDtoMapping());
        modelBuilder.ApplyConfiguration(new VehicleDtoMapping());

        modelBuilder.Entity<DriverDto>()
            .HasQueryFilter(field => field.DeletedAt == null);
        
        modelBuilder.Entity<DriverAvailabilityDto>()
            .HasQueryFilter(field => field.DeletedAt == null);
        
        modelBuilder.Entity<DriverLocationDto>()
            .HasQueryFilter(field => field.DeletedAt == null);

        modelBuilder.Entity<VehicleDto>()
            .HasQueryFilter(field => field.DeletedAt == null);

        modelBuilder.HasDefaultSchema(Constants.SchemaName);
    }
}
