using Microsoft.EntityFrameworkCore;
using TaxiService.Common.BaseInfrastructure.Extensions;
using TaxiService.Modules.Ride.Domain.Entities;
using TaxiService.Modules.Ride.Infrastructure.Mappings.Domain;

namespace TaxiService.Modules.Ride.Infrastructure.Contexts;

[ExcludeFromCodeCoverage]
public class RidesDbContext(
    DbContextOptions<RidesDbContext> options)
    : DbContext(options)
{
    private const string Schema = "ride";

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.ApplyConfiguration(new PositionMapping());
        modelBuilder.ApplyConfiguration(new RideMapping());

        modelBuilder.ApplySoftDeleteQueryFilter<Position>();
        modelBuilder.ApplySoftDeleteQueryFilter<Domain.Aggregates.Ride>();

        modelBuilder.IgnoreClass();

        modelBuilder.HasDefaultSchema(Schema);
    }
}
