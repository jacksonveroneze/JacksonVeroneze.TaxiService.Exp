using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Mappings;

public class PhoneMapping : IEntityTypeConfiguration<PhoneEntity>
{
    public void Configure(EntityTypeBuilder<PhoneEntity> builder)
    {
        builder.ToTable("phone");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.OwnsOne(conf => conf.Phone, conf =>
        {
            conf.WithOwner();

            conf.Property(prop => prop.Value)
                .HasColumnName("value")
                .HasMaxLength(11)
                .IsRequired();
        });

        builder.ConfigureDefaultFiledsMapping();
    }
}
