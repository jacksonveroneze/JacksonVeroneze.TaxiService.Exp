using Microsoft.EntityFrameworkCore;

using TaxiService.Modules.Rides.Application.v1.Ride.Common.Models;
using TaxiService.Modules.Rides.Infrastructure.Mappings.ReadModels;

namespace TaxiService.Modules.Rides.Infrastructure.Contexts;

[ExcludeFromCodeCoverage]
public class RidesReadDbContext(
    DbContextOptions<RidesReadDbContext> options)
    : DbContext(options)
{
    private const string Schema = "ride";
    
    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.ApplyConfiguration(new RideDtoMapping());

        modelBuilder.Entity<RideDto>()
            .HasQueryFilter(field => field.DeletedAt == null);
        
        modelBuilder.HasDefaultSchema(Schema);
    }
}
