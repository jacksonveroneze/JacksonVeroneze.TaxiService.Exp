using Microsoft.EntityFrameworkCore;

using TaxiService.Modules.Drivers.Application.v1.Driver.Common.Models;
using TaxiService.Modules.Drivers.Application.v1.Vehicle.Common.Models;
using TaxiService.Modules.Drivers.Infrastructure.Mappings.ReadModels;

namespace TaxiService.Modules.Drivers.Infrastructure.Contexts;

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
        modelBuilder.ApplyConfiguration(new VehicleDtoMapping());

        modelBuilder.Entity<DriverDto>()
            .HasQueryFilter(field => field.DeletedAt == null);
        
        modelBuilder.Entity<VehicleDto>()
            .HasQueryFilter(field => field.DeletedAt == null);

        modelBuilder.HasDefaultSchema("driver");
    }
}
