using Microsoft.EntityFrameworkCore;
using TaxiService.Common.BaseInfrastructure.Extensions;
using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Infrastructure.EfCore.Contexts;

[ExcludeFromCodeCoverage]
public class PassengersDbContext(
    DbContextOptions<PassengersDbContext> options)
    : DbContext(options)
{
    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(PassengersDbContext).Assembly);

        modelBuilder.ApplySoftDeleteQueryFilter<EmailEntity>();
        modelBuilder.ApplySoftDeleteQueryFilter<UserEntity>();

        modelBuilder.IgnoreClass();

        modelBuilder.HasDefaultSchema("passenger");
    }
}