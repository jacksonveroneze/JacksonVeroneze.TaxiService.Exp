using Microsoft.EntityFrameworkCore;
using TaxiService.Modules.Passenger.Application.v1.Passenger.Common.Models;
using TaxiService.Modules.Passenger.Infrastructure.Mappings.ReadModels;

namespace TaxiService.Modules.Passenger.Infrastructure.Contexts;

[ExcludeFromCodeCoverage]
public class PassengersReadDbContext(
    DbContextOptions<PassengersReadDbContext> options)
    : DbContext(options)
{
    private const string Schema = "passenger";

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.ApplyConfiguration(new PassengerDtoMapping());

        modelBuilder.Entity<PassengerDto>()
            .HasQueryFilter(field => field.DeletedAt == null);

        modelBuilder.HasDefaultSchema(Schema);
    }
}
