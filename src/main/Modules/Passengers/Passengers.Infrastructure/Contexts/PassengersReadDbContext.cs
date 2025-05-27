using Microsoft.EntityFrameworkCore;

using TaxiService.Modules.Passengers.Application.v1.Models.Passenger;
using TaxiService.Modules.Passengers.Infrastructure.Mappings.ReadModels;

namespace TaxiService.Modules.Passengers.Infrastructure.Contexts;

[ExcludeFromCodeCoverage]
public class PassengersReadDbContext(
    DbContextOptions<PassengersReadDbContext> options)
    : DbContext(options)
{
    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.ApplyConfiguration(new PassengerDtoMapping());
        
        modelBuilder.Entity<PassengerDto>()
            .HasQueryFilter(field => field.DeletedAt == null);

        modelBuilder.HasDefaultSchema("passenger");
    }
}
