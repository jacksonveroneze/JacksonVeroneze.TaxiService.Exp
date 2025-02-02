using JacksonVeroneze.NET.DomainObjects.Domain;
using JacksonVeroneze.NET.DomainObjects.Messaging;
using Microsoft.EntityFrameworkCore;
using TaxiService.Modules.Payments.Domain.Entities;

namespace TaxiService.Modules.Payments.Infrastructure.EfCore.Contexts;

[ExcludeFromCodeCoverage]
public class PaymentsDbContext(
    DbContextOptions<PaymentsDbContext> options)
    : DbContext(options)
{
    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(PaymentsDbContext).Assembly);

        ApplyQueryFilter(modelBuilder);
        IgnoreClass(modelBuilder);

        modelBuilder.HasDefaultSchema("public");
    }

    private static void ApplyQueryFilter(
        ModelBuilder modelBuilder)
    {
        ApplySoftDeleteQueryFilter<PaymentEntity>(modelBuilder);
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
    }
}