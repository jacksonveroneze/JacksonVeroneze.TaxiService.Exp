using JacksonVeroneze.NET.DomainObjects.Messaging;
using JacksonVeroneze.TaxiService.Exp.Application.Interfaces.Tenant;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.Contexts;

[ExcludeFromCodeCoverage]
public class ReadApplicationDbContext(
    DbContextOptions<ReadApplicationDbContext> options,
    ITenantService tenantService)
    : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
    }

    public override int SaveChanges()
    {
        throw new InvalidOperationException(
            "This context is read-only.");
    }
}