using Microsoft.EntityFrameworkCore;
using TaxiService.Common.BaseInfrastructure.Extensions;
using TaxiService.Modules.Rides.Domain.Entities;
using TaxiService.Modules.Rides.Infrastructure.Mappings;
using TaxiService.Modules.Rides.Infrastructure.Mappings.Domain;

namespace TaxiService.Modules.Rides.Infrastructure.Contexts;

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
        
        modelBuilder.ApplySoftDeleteQueryFilter<PositionEntity>();
        modelBuilder.ApplySoftDeleteQueryFilter<RideEntity>();

        modelBuilder.IgnoreClass();

        modelBuilder.HasDefaultSchema(Schema);
    }
}
