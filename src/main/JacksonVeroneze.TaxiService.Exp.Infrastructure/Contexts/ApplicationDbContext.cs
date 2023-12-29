using JacksonVeroneze.NET.DomainObjects.Messaging;
using JacksonVeroneze.TaxiService.Exp.Application.Interfaces.Tenant;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.Contexts;

[ExcludeFromCodeCoverage]
public class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options,
    ITenantService tenantService)
    : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ReadApplicationDbContext).Assembly);

        modelBuilder.Ignore<Event>();

        modelBuilder.HasDefaultSchema("public");

        modelBuilder
            .Entity<UserEntity>()
            .HasQueryFilter(filter => filter.TenantId ==
                tenantService.GetTenantId() && filter.DeletedAt == null);

        modelBuilder
            .Entity<EmailEntity>()
            .HasQueryFilter(filter => filter.TenantId ==
                tenantService.GetTenantId() && filter.DeletedAt == null);

        modelBuilder
            .Entity<RideEntity>()
            .HasQueryFilter(filter => filter.TenantId ==
                tenantService.GetTenantId() && filter.DeletedAt == null);

        modelBuilder
            .Entity<PositionEntity>()
            .HasQueryFilter(filter => filter.TenantId ==
                tenantService.GetTenantId() && filter.DeletedAt == null);

        modelBuilder
            .Entity<TransactionEntity>()
            .HasQueryFilter(filter => filter.TenantId ==
                tenantService.GetTenantId() && filter.DeletedAt == null);
    }

    public override Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        IEnumerable<EntityEntry<BaseEntityAggregateRoot>> entityEntries =
            ChangeTracker.Entries<BaseEntityAggregateRoot>();

        foreach (EntityEntry<BaseEntityAggregateRoot> entry in entityEntries
                     .Where(item => item.State == EntityState.Added))
        {
            entry.Entity.TenantId = tenantService.GetTenantId();
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}