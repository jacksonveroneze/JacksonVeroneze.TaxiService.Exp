using JacksonVeroneze.NET.DomainObjects.Domain;
using JacksonVeroneze.NET.DomainObjects.Messaging;
using Microsoft.EntityFrameworkCore;
using TaxiService.Modules.Passengers.Domain.Entities;
using TaxiService.Modules.Passengers.Domain.ValueObjects;

namespace TaxiService.Modules.Passengers.Infrastructure.EfCore.Contexts;

[ExcludeFromCodeCoverage]
public class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options)
    : DbContext(options)
{
    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationDbContext).Assembly);

        ApplyQueryFilter(modelBuilder);
        IgnoreClass(modelBuilder);

        modelBuilder.HasDefaultSchema("public");
    }

    private static void ApplyQueryFilter(
        ModelBuilder modelBuilder)
    {
        ApplySoftDeleteQueryFilter<DriverEntity>(modelBuilder);
        ApplySoftDeleteQueryFilter<EmailEntity>(modelBuilder);
        ApplySoftDeleteQueryFilter<PositionEntity>(modelBuilder);
        ApplySoftDeleteQueryFilter<RideEntity>(modelBuilder);
        ApplySoftDeleteQueryFilter<TransactionEntity>(modelBuilder);
        ApplySoftDeleteQueryFilter<UserEntity>(modelBuilder);
    }

    private static void ApplySoftDeleteQueryFilter<TEntity>(
        ModelBuilder modelBuilder) where TEntity : Entity
    {
        modelBuilder.Entity<TEntity>()
            .HasQueryFilter(field => field.DeletedAt == null);
    }

    private static void IgnoreClass(
        ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<Event>();
        modelBuilder.Ignore<DomainEvent>();
        modelBuilder.Ignore<EmailValueObject>();
    }
}