using JacksonVeroneze.NET.DomainObjects.Messaging;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Infrastructure.Contexts.Extensions;
using Microsoft.EntityFrameworkCore;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Contexts;

public class TemplateWebApiContext : DbContext
{
    public TemplateWebApiContext(DbContextOptions<TemplateWebApiContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(TemplateWebApiContext).Assembly);

        modelBuilder.Ignore<Event>();

        modelBuilder.HasDefaultSchema("public");

        modelBuilder.AddDeletedAtFilter<UserEntity, Guid>();
        modelBuilder.AddDeletedAtFilter<EmailEntity, Guid>();
        modelBuilder.AddDeletedAtFilter<PhoneEntity, Guid>();
    }
}
