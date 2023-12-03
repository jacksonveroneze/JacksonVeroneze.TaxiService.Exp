using JacksonVeroneze.NET.DomainObjects.Messaging;
using JacksonVeroneze.TaxiService.Exp.Application.Interfaces.Tenant;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.Contexts;

[ExcludeFromCodeCoverage]
public class ReadApplicationDbContext(
    DbContextOptions<ReadApplicationDbContext> options,
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

        modelBuilder
            .Entity<UserEntity>()
            .HasQueryFilter(filter => filter.TenantId ==
                _tenantId && filter.DeletedAt == null);

        modelBuilder
            .Entity<EmailEntity>()
            .HasQueryFilter(filter => filter.TenantId ==
                _tenantId && filter.DeletedAt == null);
    }

    public override int SaveChanges()
    {
        throw new InvalidOperationException(
            "This context is read-only.");
    }
}
