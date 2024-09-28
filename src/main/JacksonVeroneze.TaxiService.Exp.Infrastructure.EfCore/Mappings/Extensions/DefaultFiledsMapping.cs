using JacksonVeroneze.TaxiService.Exp.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.Mappings.Extensions;

[ExcludeFromCodeCoverage]
public static class EntityTypeBuilderExtensions
{
    public static void ConfigureDefaultFieldsMapping<TEntity>(
        this EntityTypeBuilder<TEntity> builder)
        where TEntity : BaseEntityAggregateRoot
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Property(c => c.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("NOW()");

        builder.Property(c => c.UpdatedAt);

        builder.Property(c => c.DeletedAt);

        builder.Property(c => c.TenantId)
            .IsRequired();

        builder.Property(c => c.Version)
            .HasDefaultValue(1)
            .IsConcurrencyToken()
            .IsRequired();

        builder.HasIndex(c => c.TenantId);

        builder.Ignore(x => x.Events);
    }
}