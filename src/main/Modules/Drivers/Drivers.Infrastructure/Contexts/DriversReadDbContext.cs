using Microsoft.EntityFrameworkCore;
using TaxiService.Modules.Drivers.Application.v1.Driver;
using TaxiService.Modules.Drivers.Infrastructure.Mappings;
using TaxiService.Modules.Drivers.Infrastructure.Mappings.ReadModels;

namespace TaxiService.Modules.Drivers.Infrastructure.Contexts;

[ExcludeFromCodeCoverage]
public class DriversReadDbContext(
    DbContextOptions<DriversReadDbContext> options)
    : DbContext(options)
{
    public DbSet<DriverDto> Drivers { get; set; }
    
    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.ApplyConfiguration(new DriverDtoMapping());

        modelBuilder.HasDefaultSchema("driver");
    }
}