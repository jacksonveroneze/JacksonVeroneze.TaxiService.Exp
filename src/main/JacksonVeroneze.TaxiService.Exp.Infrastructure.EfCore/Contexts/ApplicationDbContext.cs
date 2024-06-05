using JacksonVeroneze.NET.DomainObjects.Messaging;
using JacksonVeroneze.TaxiService.Exp.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.Contexts;

[ExcludeFromCodeCoverage]
public class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options)
    : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ReadApplicationDbContext).Assembly);

        modelBuilder.Ignore<Event>();
        modelBuilder.Ignore<DomainEvent>();
        modelBuilder.Ignore<EmailValueObject>();

        modelBuilder.HasDefaultSchema("public");
    }
}