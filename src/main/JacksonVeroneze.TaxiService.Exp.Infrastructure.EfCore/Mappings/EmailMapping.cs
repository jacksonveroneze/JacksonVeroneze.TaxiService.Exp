using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.Mappings;

[ExcludeFromCodeCoverage]
public class EmailMapping : IEntityTypeConfiguration<EmailEntity>
{
    public void Configure(EntityTypeBuilder<EmailEntity> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("email");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Property(c => c.UserId)
            .IsRequired();

        builder.ComplexProperty(conf => conf.Email, conf =>
        {
            conf.Property(prop => prop.Value)
                .HasColumnName("value")
                .HasMaxLength(100);
        });

        builder.ConfigureDefaultFiledsMapping();
    }
}