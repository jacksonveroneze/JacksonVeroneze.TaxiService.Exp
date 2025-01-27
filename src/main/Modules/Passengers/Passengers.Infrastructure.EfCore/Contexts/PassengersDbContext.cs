using JacksonVeroneze.NET.DomainObjects.Domain;
using JacksonVeroneze.NET.DomainObjects.Messaging;
using Microsoft.EntityFrameworkCore;
using TaxiService.Modules.Passengers.Domain.Entities;
using TaxiService.Modules.Passengers.Domain.ValueObjects;

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

        ApplyQueryFilter(modelBuilder);
        IgnoreClass(modelBuilder);

        modelBuilder.HasDefaultSchema("public");
    }

    private static void ApplyQueryFilter(
        ModelBuilder modelBuilder)
    {
        ApplySoftDeleteQueryFilter<EmailEntity>(modelBuilder);
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