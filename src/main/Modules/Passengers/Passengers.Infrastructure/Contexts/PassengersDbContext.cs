using Microsoft.EntityFrameworkCore;
using TaxiService.Common.BaseInfrastructure.Extensions;
using TaxiService.Modules.Passengers.Domain.Entities;
using TaxiService.Modules.Passengers.Infrastructure.Mappings.Domain;

namespace TaxiService.Modules.Passengers.Infrastructure.Contexts;

[ExcludeFromCodeCoverage]
public class PassengersDbContext(
    DbContextOptions<PassengersDbContext> options)
    : DbContext(options)
{
    private const string Schema = "driver";
    
    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.ApplyConfiguration(new EmailMapping());
        modelBuilder.ApplyConfiguration(new PassengerMapping());
        modelBuilder.ApplyConfiguration(new OutboxMessageMapping());

        modelBuilder.ApplySoftDeleteQueryFilter<EmailEntity>();
        modelBuilder.ApplySoftDeleteQueryFilter<PassengerEntity>();

        modelBuilder.IgnoreClass();

        modelBuilder.HasDefaultSchema(Schema);
    }
}
