using JacksonVeroneze.TemplateWebApi.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Mappings;

[ExcludeFromCodeCoverage]
public static class DefaultFiledsMapping
{
    public static void ConfigureDefaultFiledsMapping<TEntity>(
        this EntityTypeBuilder<TEntity> builder)
        where TEntity : BaseEntityAggregateRoot
    {
        builder.Property(c => c.CreatedAt)
            .IsRequired();

        builder.Property(c => c.UpdatedAt);

        builder.Property(c => c.DeletedAt);
        builder.Property(c => c.TenantId);

        builder.Property(c => c.Version)
            .IsConcurrencyToken()
            .IsRequired();
    }
}
