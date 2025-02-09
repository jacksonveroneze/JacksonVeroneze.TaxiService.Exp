using Microsoft.EntityFrameworkCore;
using TaxiService.Common.BaseInfrastructure.Extensions;
using TaxiService.Modules.Rides.Domain.Entities;

namespace TaxiService.Modules.Rides.Infrastructure.Contexts;

[ExcludeFromCodeCoverage]
public class RidesDbContext(
    DbContextOptions<RidesDbContext> options)
    : DbContext(options)
{
    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(RidesDbContext).Assembly);

        modelBuilder.ApplySoftDeleteQueryFilter<PositionEntity>();
        modelBuilder.ApplySoftDeleteQueryFilter<RideEntity>();

        modelBuilder.IgnoreClass();

        modelBuilder.HasDefaultSchema("ride");
    }
}