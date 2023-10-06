using JacksonVeroneze.TemplateWebApi.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Contexts.Extensions;

[ExcludeFromCodeCoverage]
public static class ModelBuilderExtensions
{
    public static ModelBuilder AddFilter<TEntity>(
        this ModelBuilder modelBuilder, Guid tenant)
        where TEntity : BaseEntityAggregateRoot
    {
        modelBuilder.Entity<TEntity>()
            .HasQueryFilter(filter => filter.TenantId == tenant &&
                                      filter.DeletedAt == null);

        return modelBuilder;
    }
}
