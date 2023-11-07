using JacksonVeroneze.NET.DomainObjects.Messaging;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Tenant;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Contexts;

[ExcludeFromCodeCoverage]
public class ApplicationDbContext : DbContext
{
    private readonly Guid _tenantId;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        ITenantService tenantService) : base(options)
    {
        //_tenantId = tenantService.TenantId;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationDbContext).Assembly);

        modelBuilder.Ignore<Event>();

        modelBuilder.HasDefaultSchema("public");

        modelBuilder
            .Entity<UserEntity>()
            .HasQueryFilter(filter => filter.TenantId == _tenantId && filter.DeletedAt == null);

        modelBuilder
            .Entity<EmailEntity>()
            .HasQueryFilter(filter => filter.TenantId == _tenantId && filter.DeletedAt == null);

        modelBuilder
            .Entity<PhoneEntity>()
            .HasQueryFilter(filter => filter.TenantId == _tenantId && filter.DeletedAt == null);
    }

    public override Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        IEnumerable<EntityEntry<BaseEntityAggregateRoot>> entityEntries =
            ChangeTracker.Entries<BaseEntityAggregateRoot>();

        foreach (EntityEntry<BaseEntityAggregateRoot> entry in entityEntries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.TenantId = _tenantId;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
