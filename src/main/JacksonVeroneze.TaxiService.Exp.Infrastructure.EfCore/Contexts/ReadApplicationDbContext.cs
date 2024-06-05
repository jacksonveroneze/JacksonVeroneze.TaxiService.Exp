using JacksonVeroneze.NET.DomainObjects.Messaging;
using JacksonVeroneze.TaxiService.Exp.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.Contexts;

[ExcludeFromCodeCoverage]
public class ReadApplicationDbContext(
    DbContextOptions<ReadApplicationDbContext> options)
    : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ReadApplicationDbContext).Assembly);

        modelBuilder.Ignore<Event>();
        modelBuilder.Ignore<DomainEvent>();
        modelBuilder.Ignore<EmailValueObject>();

        modelBuilder.HasDefaultSchema("public");
    }

    public override int SaveChanges()
    {
        throw new InvalidOperationException(
            "This context is read-only.");
    }
}