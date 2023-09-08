using JacksonVeroneze.NET.DomainObjects.Domain;
using Microsoft.EntityFrameworkCore;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Contexts.EntityFramework.Extensions;

[ExcludeFromCodeCoverage]
public static class ModelBuilderExtensions
{
    public static ModelBuilder AddDeletedAtFilter<TEntity, TKey>(
        this ModelBuilder modelBuilder) where TEntity : Entity<TKey>
    {
        modelBuilder.Entity<TEntity>()
            .HasQueryFilter(x => x.DeletedAt == null);

        return modelBuilder;
    }
}
