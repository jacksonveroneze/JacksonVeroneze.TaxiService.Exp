using Microsoft.EntityFrameworkCore;
using TaxiService.Modules.Ride.Application.v1.Ride.Common.Models;
using TaxiService.Modules.Ride.Infrastructure.Mappings.ReadModels;

namespace TaxiService.Modules.Ride.Infrastructure.Contexts;

[ExcludeFromCodeCoverage]
public class RidesReadDbContext(
    DbContextOptions<RidesReadDbContext> options)
    : DbContext(options)
{
    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.ApplyConfiguration(new RideDtoMapping());

        modelBuilder.Entity<RideDto>()
            .HasQueryFilter(field => field.DeletedAt == null);

        modelBuilder.HasDefaultSchema(Constants.SchemaName);
    }
}
