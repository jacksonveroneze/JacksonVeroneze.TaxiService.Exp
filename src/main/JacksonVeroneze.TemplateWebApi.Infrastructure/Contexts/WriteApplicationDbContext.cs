using JacksonVeroneze.NET.DomainObjects.Messaging;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Tenant;
using JacksonVeroneze.TemplateWebApi.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Contexts;

[ExcludeFromCodeCoverage]
public class WriteApplicationDbContext(
    DbContextOptions<WriteApplicationDbContext> options,
    ITenantService tenantService)
    : DbContext(options)
{
    private readonly Guid _tenantId = tenantService.TenantId;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ReadApplicationDbContext).Assembly);

        modelBuilder.Ignore<Event>();

        modelBuilder.HasDefaultSchema("public");
    }

    public override Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        IEnumerable<EntityEntry<BaseEntityAggregateRoot>> entityEntries =
            ChangeTracker.Entries<BaseEntityAggregateRoot>();

        foreach (EntityEntry<BaseEntityAggregateRoot> entry in entityEntries
                     .Where(item => item.State == EntityState.Added))
        {
            entry.Entity.TenantId = _tenantId;
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
