using JacksonVeroneze.TaxiService.Exp.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.Mappings;

[ExcludeFromCodeCoverage]
public static class DefaultFiledsMapping
{
    public static void ConfigureDefaultFiledsMapping<TEntity>(
        this EntityTypeBuilder<TEntity> builder)
        where TEntity : BaseEntityAggregateRoot
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Property(c => c.CreatedAt)
            .IsRequired();

        builder.Property(c => c.UpdatedAt);

        builder.Property(c => c.DeletedAt);

        builder.Property(c => c.TenantId)
            .IsRequired();

        builder.Property(c => c.Version)
            .IsConcurrencyToken()
            .IsRequired();

        builder.Ignore(x => x.Events);
    }
}
