using JacksonVeroneze.NET.DomainObjects.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Mappings;

[ExcludeFromCodeCoverage]
public static class DefaultFiledsMapping
{
    public static void ConfigureDefaultFiledsMapping<T>(
        this EntityTypeBuilder<T> builder) where T : Entity<Guid>
    {
        builder.Property(c => c.CreatedAt)
            .IsRequired();

        builder.Property(c => c.UpdatedAt);

        builder.Property(c => c.DeletedAt);

        builder.Property(c => c.Version)
            .IsConcurrencyToken()
            .IsRequired();
    }
}
